using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
namespace MeisterSDKReporting.MeisterModel
{
    public class EdmHeader
    {
        private static MapperConfiguration configuration { get; set; }
        private static Mapper mapper { get; set; }
        public EdmHeader()
        {
            configuration = new MapperConfiguration(cfg => cfg.CreateMap<EdmInternal, Edm>());
        }
        public List<Edm> Edms { get; set; }
        /// <summary>
        /// Edm as a List
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static List<Edm> ToEdmList(string json)
        {
            EdmHeaderInternal headerInternal = JsonConvert.DeserializeObject<EdmHeaderInternal>(json, Converter.Settings);
            EdmHeader edmHeader = new EdmHeader();
            edmHeader.Edms = new List<Edm>();
            mapper = new Mapper(configuration);
            foreach (var item in headerInternal.edmInternals)
                edmHeader.Edms.Add(mapper.Map<Edm>(item));
            return edmHeader.Edms;
        }
        /// <summary>
        /// Edm as a datatable
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable ToEdmDataTable(string json)
        {
            List<Edm> edms = ToEdmList(json);
            return CreateDataTable<Edm>(edms);
        }
        /// <summary>
        /// Builds a datable from the List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static DataTable CreateDataTable<T>(IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();
            DataTable dataTable = new DataTable();
            foreach (PropertyInfo info in properties)
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                    values[i] = properties[i].GetValue(entity);
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
    }
    /// <summary>
    /// Header node
    /// </summary>
    internal partial class EdmHeaderInternal
    {
        [JsonProperty("EDM")]
        public List<EdmInternal> edmInternals { get; set; }
    }
    /// <summary>
    /// The Edm line item 
    /// </summary>
    internal partial class EdmInternal
    {
        [JsonProperty("ROW_POS")]
        public int RowPos { get; set; }
        [JsonProperty("COL_POS")]
        public int ColPos { get; set; }
        [JsonProperty("FIELDNAME")]
        public string Fieldname { get; set; }
        [JsonProperty("TABNAME")]
        public string Tabname { get; set; }
        [JsonProperty("CURRENCY")]
        public string Currency { get; set; }
        [JsonProperty("CFIELDNAME")]
        public string Cfieldname { get; set; }
        [JsonProperty("QUANTITY")]
        public string Quantity { get; set; }
        [JsonProperty("QFIELDNAME")]
        public string Qfieldname { get; set; }
        [JsonProperty("IFIELDNAME")]
        public string Ifieldname { get; set; }
        [JsonProperty("ROUND")]
        public int Round { get; set; }
        [JsonProperty("EXPONENT")]
        public string Exponent { get; set; }
        [JsonProperty("KEY")]
        public string Key { get; set; }
        [JsonProperty("KEY_SEL")]
        public string KeySel { get; set; }
        [JsonProperty("ICON")]
        public string Icon { get; set; }
        [JsonProperty("SYMBOL")]
        public string Symbol { get; set; }
        [JsonProperty("CHECKBOX")]
        public string Checkbox { get; set; }
        [JsonProperty("JUST")]
        public string Just { get; set; }
        [JsonProperty("LZERO")]
        public string Lzero { get; set; }
        [JsonProperty("NO_SIGN")]
        public string NoSign { get; set; }
        [JsonProperty("NO_ZERO")]
        public string NoZero { get; set; }
        [JsonProperty("NO_CONVEXT")]
        public string NoConvext { get; set; }
        [JsonProperty("EDIT_MASK")]
        public string EditMask { get; set; }
        [JsonProperty("EMPHASIZE")]
        public string Emphasize { get; set; }
        [JsonProperty("FIX_COLUMN")]
        public string FixColumn { get; set; }
        [JsonProperty("DO_SUM")]
        public string DoSum { get; set; }
        [JsonProperty("NO_SUM")]
        public string NoSum { get; set; }
        [JsonProperty("NO_OUT")]
        public string NoOut { get; set; }
        [JsonProperty("TECH")]
        public string Tech { get; set; }
        [JsonProperty("OUTPUTLEN")]
        public int Outputlen { get; set; }
        [JsonProperty("CONVEXIT")]
        public string Convexit { get; set; }
        [JsonProperty("SELTEXT")]
        public string Seltext { get; set; }
        [JsonProperty("TOOLTIP")]
        public string Tooltip { get; set; }
        [JsonProperty("ROLLNAME")]
        public string Rollname { get; set; }
        [JsonProperty("DATATYPE")]
        public string Datatype { get; set; }
        [JsonProperty("INTTYPE")]
        public string Inttype { get; set; }
        [JsonProperty("INTLEN")]
        public int Intlen { get; set; }
        [JsonProperty("LOWERCASE")]
        public string Lowercase { get; set; }
        [JsonProperty("REPTEXT")]
        public string Reptext { get; set; }
        [JsonProperty("HIER_LEVEL")]
        public int HierLevel { get; set; }
        [JsonProperty("REPREP")]
        public string Reprep { get; set; }
        [JsonProperty("DOMNAME")]
        public string Domname { get; set; }
        [JsonProperty("SP_GROUP")]
        public string SpGroup { get; set; }
        [JsonProperty("HOTSPOT")]
        public string Hotspot { get; set; }
        [JsonProperty("DFIELDNAME")]
        public string Dfieldname { get; set; }
        [JsonProperty("COL_ID")]
        public int ColId { get; set; }
        [JsonProperty("F4AVAILABL")]
        public string F4Availabl { get; set; }
        [JsonProperty("AUTO_VALUE")]
        public string AutoValue { get; set; }
        [JsonProperty("CHECKTABLE")]
        public string Checktable { get; set; }
        [JsonProperty("VALEXI")]
        public string Valexi { get; set; }
        [JsonProperty("WEB_FIELD")]
        public string WebField { get; set; }
        [JsonProperty("HREF_HNDL")]
        public int HrefHndl { get; set; }
        [JsonProperty("STYLE")]
        public string Style { get; set; }
        [JsonProperty("STYLE2")]
        public string Style2 { get; set; }
        [JsonProperty("STYLE3")]
        public string Style3 { get; set; }
        [JsonProperty("STYLE4")]
        public string Style4 { get; set; }
        [JsonProperty("DRDN_HNDL")]
        public int DrdnHndl { get; set; }
        [JsonProperty("DRDN_FIELD")]
        public string DrdnField { get; set; }
        [JsonProperty("NO_MERGING")]
        public string NoMerging { get; set; }
        [JsonProperty("H_FTYPE")]
        public string HFtype { get; set; }
        [JsonProperty("COL_OPT")]
        public string ColOpt { get; set; }
        [JsonProperty("NO_INIT_CH")]
        public string NoInitCh { get; set; }
        [JsonProperty("DRDN_ALIAS")]
        public string DrdnAlias { get; set; }
        [JsonProperty("DECFLOAT_STYLE")]
        public int DecfloatStyle { get; set; }
        [JsonProperty("PARAMETER0")]
        public string Parameter0 { get; set; }
        [JsonProperty("PARAMETER1")]
        public string Parameter1 { get; set; }
        [JsonProperty("PARAMETER2")]
        public string Parameter2 { get; set; }
        [JsonProperty("PARAMETER3")]
        public string Parameter3 { get; set; }
        [JsonProperty("PARAMETER4")]
        public string Parameter4 { get; set; }
        [JsonProperty("PARAMETER5")]
        public int Parameter5 { get; set; }
        [JsonProperty("PARAMETER6")]
        public int Parameter6 { get; set; }
        [JsonProperty("PARAMETER7")]
        public int Parameter7 { get; set; }
        [JsonProperty("PARAMETER8")]
        public int Parameter8 { get; set; }
        [JsonProperty("PARAMETER9")]
        public int Parameter9 { get; set; }
        [JsonProperty("REF_FIELD")]
        public string RefField { get; set; }
        [JsonProperty("REF_TABLE")]
        public string RefTable { get; set; }
        [JsonProperty("TXT_FIELD")]
        public string TxtField { get; set; }
        [JsonProperty("ROUNDFIELD")]
        public string Roundfield { get; set; }
        [JsonProperty("DECIMALS_O")]
        public string DecimalsO { get; set; }
        [JsonProperty("DECMLFIELD")]
        public string Decmlfield { get; set; }
        [JsonProperty("DD_OUTLEN")]
        public int DdOutlen { get; set; }
        [JsonProperty("DECIMALS")]
        public int Decimals { get; set; }
        [JsonProperty("COLTEXT")]
        public string Coltext { get; set; }
        [JsonProperty("SCRTEXT_L")]
        public string ScrtextL { get; set; }
        [JsonProperty("SCRTEXT_M")]
        public string ScrtextM { get; set; }
        [JsonProperty("SCRTEXT_S")]
        public string ScrtextS { get; set; }
        [JsonProperty("COLDDICTXT")]
        public string Colddictxt { get; set; }
        [JsonProperty("SELDDICTXT")]
        public string Selddictxt { get; set; }
        [JsonProperty("TIPDDICTXT")]
        public string Tipddictxt { get; set; }
        [JsonProperty("EDIT")]
        public string Edit { get; set; }
        [JsonProperty("TECH_COL")]
        public int TechCol { get; set; }
        [JsonProperty("TECH_FORM")]
        public int TechForm { get; set; }
        [JsonProperty("TECH_COMP")]
        public string TechComp { get; set; }
        [JsonProperty("HIER_CPOS")]
        public int HierCpos { get; set; }
        [JsonProperty("H_COL_KEY")]
        public string HColKey { get; set; }
        [JsonProperty("H_SELECT")]
        public string HSelect { get; set; }
        [JsonProperty("DD_ROLL")]
        public string DdRoll { get; set; }
        [JsonProperty("DRAGDROPID")]
        public int Dragdropid { get; set; }
        [JsonProperty("MAC")]
        public string Mac { get; set; }
        [JsonProperty("INDX_FIELD")]
        public int IndxField { get; set; }
        [JsonProperty("INDX_CFIEL")]
        public int IndxCfiel { get; set; }
        [JsonProperty("INDX_QFIEL")]
        public int IndxQfiel { get; set; }
        [JsonProperty("INDX_IFIEL")]
        public int IndxIfiel { get; set; }
        [JsonProperty("INDX_ROUND")]
        public int IndxRound { get; set; }
        [JsonProperty("INDX_DECML")]
        public int IndxDecml { get; set; }
        [JsonProperty("GET_STYLE")]
        public string GetStyle { get; set; }
        [JsonProperty("MARK")]
        public string Mark { get; set; }
    }
    public partial class Edm
    {
        [JsonProperty("ROW_POS")]
        public int RowPos { get; set; }
        [JsonProperty("COL_POS")]
        public int ColPos { get; set; }
        [JsonProperty("FIELDNAME")]
        public string Fieldname { get; set; }
        [JsonProperty("TABNAME")]
        public string Tabname { get; set; }
        [JsonProperty("CURRENCY")]
        public string Currency { get; set; }
        [JsonProperty("CFIELDNAME")]
        public string Cfieldname { get; set; }
        [JsonProperty("QUANTITY")]
        public string Quantity { get; set; }
        [JsonProperty("QFIELDNAME")]
        public string Qfieldname { get; set; }
        [JsonProperty("IFIELDNAME")]
        public string Ifieldname { get; set; }
        [JsonProperty("ROUND")]
        public int Round { get; set; }
        [JsonProperty("EXPONENT")]
        public string Exponent { get; set; }
        [JsonProperty("KEY")]
        public string Key { get; set; }
        [JsonProperty("KEY_SEL")]
        public string KeySel { get; set; }
        [JsonProperty("ICON")]
        public string Icon { get; set; }
        [JsonProperty("SYMBOL")]
        public string Symbol { get; set; }
        [JsonProperty("CHECKBOX")]
        public string Checkbox { get; set; }
        [JsonProperty("JUST")]
        public string Just { get; set; }
        [JsonProperty("LZERO")]
        public string Lzero { get; set; }
        [JsonProperty("NO_SIGN")]
        public string NoSign { get; set; }
        [JsonProperty("NO_ZERO")]
        public string NoZero { get; set; }
        [JsonProperty("NO_CONVEXT")]
        public string NoConvext { get; set; }
        [JsonProperty("EDIT_MASK")]
        public string EditMask { get; set; }
        [JsonProperty("EMPHASIZE")]
        public string Emphasize { get; set; }
        [JsonProperty("FIX_COLUMN")]
        public string FixColumn { get; set; }
        [JsonProperty("DO_SUM")]
        public string DoSum { get; set; }
        [JsonProperty("NO_SUM")]
        public string NoSum { get; set; }
        [JsonProperty("NO_OUT")]
        public string NoOut { get; set; }
        [JsonProperty("TECH")]
        public string Tech { get; set; }
        [JsonProperty("OUTPUTLEN")]
        public int Outputlen { get; set; }
        [JsonProperty("CONVEXIT")]
        public string Convexit { get; set; }
        [JsonProperty("SELTEXT")]
        public string Seltext { get; set; }
        [JsonProperty("TOOLTIP")]
        public string Tooltip { get; set; }
        [JsonProperty("ROLLNAME")]
        public string Rollname { get; set; }
        [JsonProperty("DATATYPE")]
        public string Datatype { get; set; }
        [JsonProperty("INTTYPE")]
        public string Inttype { get; set; }
        [JsonProperty("INTLEN")]
        public int Intlen { get; set; }
        [JsonProperty("LOWERCASE")]
        public string Lowercase { get; set; }
        [JsonProperty("REPTEXT")]
        public string Reptext { get; set; }
        [JsonProperty("HIER_LEVEL")]
        public int HierLevel { get; set; }
        [JsonProperty("REPREP")]
        public string Reprep { get; set; }
        [JsonProperty("DOMNAME")]
        public string Domname { get; set; }
        [JsonProperty("SP_GROUP")]
        public string SpGroup { get; set; }
        [JsonProperty("HOTSPOT")]
        public string Hotspot { get; set; }
        [JsonProperty("DFIELDNAME")]
        public string Dfieldname { get; set; }
        [JsonProperty("COL_ID")]
        public int ColId { get; set; }
        [JsonProperty("F4AVAILABL")]
        public string F4Availabl { get; set; }
        [JsonProperty("AUTO_VALUE")]
        public string AutoValue { get; set; }
        [JsonProperty("CHECKTABLE")]
        public string Checktable { get; set; }
        [JsonProperty("VALEXI")]
        public string Valexi { get; set; }
        [JsonProperty("WEB_FIELD")]
        public string WebField { get; set; }
        [JsonProperty("HREF_HNDL")]
        public int HrefHndl { get; set; }
        [JsonProperty("STYLE")]
        public string Style { get; set; }
        [JsonProperty("STYLE2")]
        public string Style2 { get; set; }
        [JsonProperty("STYLE3")]
        public string Style3 { get; set; }
        [JsonProperty("STYLE4")]
        public string Style4 { get; set; }
        [JsonProperty("DRDN_HNDL")]
        public int DrdnHndl { get; set; }
        [JsonProperty("DRDN_FIELD")]
        public string DrdnField { get; set; }
        [JsonProperty("NO_MERGING")]
        public string NoMerging { get; set; }
        [JsonProperty("H_FTYPE")]
        public string HFtype { get; set; }
        [JsonProperty("COL_OPT")]
        public string ColOpt { get; set; }
        [JsonProperty("NO_INIT_CH")]
        public string NoInitCh { get; set; }
        [JsonProperty("DRDN_ALIAS")]
        public string DrdnAlias { get; set; }
        [JsonProperty("DECFLOAT_STYLE")]
        public int DecfloatStyle { get; set; }
        [JsonProperty("PARAMETER0")]
        public string Parameter0 { get; set; }
        [JsonProperty("PARAMETER1")]
        public string Parameter1 { get; set; }
        [JsonProperty("PARAMETER2")]
        public string Parameter2 { get; set; }
        [JsonProperty("PARAMETER3")]
        public string Parameter3 { get; set; }
        [JsonProperty("PARAMETER4")]
        public string Parameter4 { get; set; }
        [JsonProperty("PARAMETER5")]
        public int Parameter5 { get; set; }
        [JsonProperty("PARAMETER6")]
        public int Parameter6 { get; set; }
        [JsonProperty("PARAMETER7")]
        public int Parameter7 { get; set; }
        [JsonProperty("PARAMETER8")]
        public int Parameter8 { get; set; }
        [JsonProperty("PARAMETER9")]
        public int Parameter9 { get; set; }
        [JsonProperty("REF_FIELD")]
        public string RefField { get; set; }
        [JsonProperty("REF_TABLE")]
        public string RefTable { get; set; }
        [JsonProperty("TXT_FIELD")]
        public string TxtField { get; set; }
        [JsonProperty("ROUNDFIELD")]
        public string Roundfield { get; set; }
        [JsonProperty("DECIMALS_O")]
        public string DecimalsO { get; set; }
        [JsonProperty("DECMLFIELD")]
        public string Decmlfield { get; set; }
        [JsonProperty("DD_OUTLEN")]
        public int DdOutlen { get; set; }
        [JsonProperty("DECIMALS")]
        public int Decimals { get; set; }
        [JsonProperty("COLTEXT")]
        public string Coltext { get; set; }
        [JsonProperty("SCRTEXT_L")]
        public string ScrtextL { get; set; }
        [JsonProperty("SCRTEXT_M")]
        public string ScrtextM { get; set; }
        [JsonProperty("SCRTEXT_S")]
        public string ScrtextS { get; set; }
        [JsonProperty("COLDDICTXT")]
        public string Colddictxt { get; set; }
        [JsonProperty("SELDDICTXT")]
        public string Selddictxt { get; set; }
        [JsonProperty("TIPDDICTXT")]
        public string Tipddictxt { get; set; }
        [JsonProperty("EDIT")]
        public string Edit { get; set; }
        [JsonProperty("TECH_COL")]
        public int TechCol { get; set; }
        [JsonProperty("TECH_FORM")]
        public int TechForm { get; set; }
        [JsonProperty("TECH_COMP")]
        public string TechComp { get; set; }
        [JsonProperty("HIER_CPOS")]
        public int HierCpos { get; set; }
        [JsonProperty("H_COL_KEY")]
        public string HColKey { get; set; }
        [JsonProperty("H_SELECT")]
        public string HSelect { get; set; }
        [JsonProperty("DD_ROLL")]
        public string DdRoll { get; set; }
        [JsonProperty("DRAGDROPID")]
        public int Dragdropid { get; set; }
        [JsonProperty("MAC")]
        public string Mac { get; set; }
        [JsonProperty("INDX_FIELD")]
        public int IndxField { get; set; }
        [JsonProperty("INDX_CFIEL")]
        public int IndxCfiel { get; set; }
        [JsonProperty("INDX_QFIEL")]
        public int IndxQfiel { get; set; }
        [JsonProperty("INDX_IFIEL")]
        public int IndxIfiel { get; set; }
        [JsonProperty("INDX_ROUND")]
        public int IndxRound { get; set; }
        [JsonProperty("INDX_DECML")]
        public int IndxDecml { get; set; }
        [JsonProperty("GET_STYLE")]
        public string GetStyle { get; set; }
        [JsonProperty("MARK")]
        public string Mark { get; set; }
    }
}
