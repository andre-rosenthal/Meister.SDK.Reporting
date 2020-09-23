using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using MeisterCore;
using MeisterCore.Support;
using MeisterSDKReporting.MeisterModel;

namespace MeisterSDKReporting
{
    /// <summary>
    /// Extension to Enum
    /// </summary>
    public static class EnumUtil
    {
        public static IEnumerable<R> GetValues<T,R>()
        {
            return Enum.GetValues(typeof(T)).Cast<R>();
        }
        public static string DescriptionAttr<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return source.ToString();
        }
    }
    internal static class ReportEndPoints
    {
        public static string Retriever => "Meister.Reporting.Retrieve";
        public static string MyReports => "Meister.Reporting.MyReports";
        public static String Finder => "Meister.Reporting.Report.Finder";
        public static string Scheduler => "Meister.Reporting.Scheduler";
        public static String GetParameters => "Meister.Reporting.Report.Parameters";
        public static string RunReport => "Meister.Reporting.Run";
        public static string ReadVariant => "Meister.Reporting.Report.Variants";
        public static string UpsertVariant => "Meister.Reporting.Variant.Upsert";
    }
    public class MeisterSDKReporting : IDisposable
    {
        #region Private Session
        private MeisterSDKReporting()
        {
        }
        private bool disposedValue = false;
        private static MeisterSDKReporting _instance;
        private MeisterSupport.MeisterExtensions MeisterExtensions { get; set; }
        private MeisterSupport.MeisterOptions MeisterOptions { get; set; }
        private MeisterSupport.RuntimeOptions MeisterRuntimeOptios { get; set; }
        private MeisterSupport.AuthenticationModes MeisterAuthenticationModes { get; set; }
        private MeisterSupport.Languages MeisterLanguageSetting { get; set; }
        #endregion
        public static MeisterSDKReporting Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MeisterSDKReporting();
                return _instance;
            }
        }
        public string Client { get; set; }
        public Uri Gateway { get; set; }
        public MeisterSupport.Languages Language { get; set; }
        public bool IsODataV4 { get; set; }
        public AuthenticationHeaderValue AuthenticationHeaderValue { get; set; }
        public MeisterStatus MeisterStatus { get; set; }
        public enum Ranges
        {
            [Description("Equal")]
            EQ,
            [Description("Not Equal")]
            NE,
            [Description("Greater Than")]
            GT,
            [Description("Greater or Equal")]
            GE,
            [Description("Less Than")]
            LT,
            [Description("Less or Equal")]
            LE,
            [Description("Constains Pattern")]
            CP,
            [Description("Does not Contain Pattern")]
            NP,
            [Description("Between")]
            BT = 10,
            [Description("Not Between")]
            NB = 11
        }
        public bool IsStatusOK()
        {
            return (MeisterStatus.StatusCode >= HttpStatusCode.OK && MeisterStatus.StatusCode < HttpStatusCode.BadRequest);
        }
        #region Authentication
        public bool Authenticate(MeisterSupport.Languages language = MeisterSupport.Languages.CultureBased)
        {
            MeisterExtensions = MeisterSupport.MeisterExtensions.RemoveNullsAndEmptyArrays;
            MeisterOptions = MeisterSupport.MeisterOptions.None;
            if (IsODataV4)
                MeisterOptions = MeisterSupport.MeisterOptions.UseODataV4;
            MeisterRuntimeOptios = MeisterSupport.RuntimeOptions.ExecuteSync;
            MeisterLanguageSetting = language;
            Resource<dynamic, dynamic> resource = BuildResource<dynamic, dynamic>();
            MeisterStatus = resource.Authenticate();
            if (IsStatusOK())
                return true;
            else
                return false;
        }
        #endregion
        #region Endpoint calls
        /// <summary>
        /// My Reports endpoint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MyReportsResponse MyReports(MyReportsRequest request)
        {
            Resource<MyReportsRequest, MyReportsResponse> resource = BuildResource<MyReportsRequest, MyReportsResponse>();
            return ExecuteRequest<MyReportsRequest, MyReportsResponse>(resource, ReportEndPoints.MyReports, request);
        }
        /// <summary>
        /// Run scheduler endpoint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public SchedulerResponse RunScheduler(SchedulerRequest request)
        {
            Resource<SchedulerRequest, SchedulerResponse > resource = BuildResource<SchedulerRequest, SchedulerResponse>();
            return ExecuteRequest<SchedulerRequest, SchedulerResponse>(resource, ReportEndPoints.Scheduler, request);
        }
        /// <summary>
        /// Find reports endpoint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ReportFinderResponse FindReport(ReportFinderRequest request)
        {
            Resource<ReportFinderRequest, ReportFinderResponse> resource = BuildResource<ReportFinderRequest, ReportFinderResponse>();
            return ExecuteRequest<ReportFinderRequest, ReportFinderResponse>(resource, ReportEndPoints.Finder, request);
        }
        /// <summary>
        /// Read Report endpoint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ReadReportResponse ReadReport(ReadReportRequest request)
        {
            Resource<ReadReportRequest, ReadReportResponse> resource = BuildResource<ReadReportRequest, ReadReportResponse>();
            return ExecuteRequest<ReadReportRequest, ReadReportResponse>(resource, ReportEndPoints.Retriever, request);
        }
        /// <summary>
        /// Report Parameters endpoint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ReportParametersResponse ReportParameters(ReportParametersRequest request)
        {
            Resource<ReportParametersRequest, ReportParametersResponse> resource = BuildResource<ReportParametersRequest, ReportParametersResponse>();
            return ExecuteRequest<ReportParametersRequest, ReportParametersResponse>(resource, ReportEndPoints.GetParameters, request);
        }
        /// <summary>
        /// Run report endpoint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ReportRunResponse RunReport(ReportRunRequest request)
        {
            Resource<ReportRunRequest, ReportRunResponse> resource = BuildResource<ReportRunRequest, ReportRunResponse>();
            return ExecuteRequest<ReportRunRequest, ReportRunResponse>(resource, ReportEndPoints.RunReport, request);
        }
        /// <summary>
        /// Update/Insert variant endpoint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public UpsertVariantResponse UpsertVariant(UpsertVariantRequest request)
        {
            Resource<UpsertVariantRequest, UpsertVariantResponse> resource = BuildResource<UpsertVariantRequest, UpsertVariantResponse>();
            return ExecuteRequest<UpsertVariantRequest, UpsertVariantResponse>(resource, ReportEndPoints.UpsertVariant, request);
        }
        /// <summary>
        /// Read variant endpoint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public VariantResponse ReadVariant(VariantRequest request)
        {
            Resource<VariantRequest, VariantResponse> resource = BuildResource<VariantRequest, VariantResponse>();
            return ExecuteRequest<VariantRequest, VariantResponse>(resource, ReportEndPoints.ReadVariant, request);
        }
        #endregion
        #region Support Calls
        /// <summary>
        /// Get the code for SAP Parameter Kinds
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string GetParameterKinds(string text)
        {
            if (text == System.Enum.GetName(typeof(Parameter.KindTypes), Parameter.KindTypes.Selection))
                return "S";
            else
                return "P";
        }
        /// <summary>
        /// Get the text for SAP Parameter Kinds
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string GetKindName(string text)
        {
            if (text == "S")
                return System.Enum.GetName(typeof(Parameter.KindTypes), Parameter.KindTypes.Selection);
            else
                return System.Enum.GetName(typeof(Parameter.KindTypes), Parameter.KindTypes.Parameter);
        }
        /// <summary>
        /// Range Options: 
        /// if high is false
        ///     EQ, NE, GT, LE, LT,CP, and NP
        /// otherwise 
        ///     BT (BeTween) and NB (Not Between)
        /// </summary>
        /// <param name="high"></param>
        /// <returns></returns>
        private List<string> RangeOptions(bool high)
        {
            List<string> l = new List<string>();
            foreach (var item in EnumUtil.GetValues<Ranges,int>())
                if (high)
                {
                    if (item >= 10)
                        l.Add(System.Enum.GetName(typeof(Ranges), item));
                }
                else if (item < 10)
                    l.Add(System.Enum.GetName(typeof(Ranges), item));
            return l;
        }
        /// <summary>
        /// User friendly list of Ranges
        /// </summary>
        /// <returns></returns>
        public List<string> RangeDescriptions()
        {
            List<string> l = new List<string>();
            foreach (Ranges range in Enum.GetValues(typeof(Ranges)))
                l.Add(EnumUtil.DescriptionAttr<Ranges>(range));
            return l;
        }
        #endregion
        #region Runtime Calls
        private dynamic ExecuteRequest<REQ,RES>(Resource<REQ,RES> resource,string endPoint, REQ request)
        {
            try
            {
                return resource.Execute(endPoint, request);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                MeisterStatus = resource.MeisterStatus;
            }
        }
        private Resource<R,S> BuildResource<R,S>()
        {
            return new Resource<R, S>(Gateway, AuthenticationHeaderValue, Client, MeisterExtensions, MeisterOptions, MeisterAuthenticationModes, MeisterRuntimeOptios, MeisterLanguageSetting);
        }
        #endregion
        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

}

