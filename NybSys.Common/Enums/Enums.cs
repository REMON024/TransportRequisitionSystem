using System.ComponentModel;

namespace NybSys.Common.Enums
{
    public class Enums
    {
        public enum Status
        {
            Active = 1,
            Inactive = 2,
            Deleted = 9,
            Block = 4
        }
        public enum RequisitionStatus
        {
            Submitted = 1,
            Approved = 2,
            Rejected = 3,
            Closed = 4,
            Cancel = 5

        }
        public enum TokenAlgorithm
        {
            [Description("HS256")]
            HmacSha256,
            [Description("HS384")]
            HmacSha384,
            [Description("HS512")]
            HmacSha512,
            [Description("RS256")]
            RsaSha256,
            [Description("RS384")]
            RsaSha384,
            [Description("RS512")]
            RsaSha512,
            [Description("ES256")]
            EcdsaSha256,
            [Description("ES384")]
            EcdsaSha384,
            [Description("ES512")]
            EcdsaSha512,
            [Description("PS256")]
            RsaSsaPssSha256,
            [Description("PS384")]
            RsaSsaPssSha384
        }
    }

    public enum Action
    {
        Insert=1,
        Update,
        Delete,
        View,
        Other
    }

    public enum Module
    {
        Web=1,
        Desktop,
        Mobile
    }

    public enum LogType
    {
        SecurityLog=1,
        ErrorLog,
        SystemLog,
        DbQuery,
        Other
    }
}
