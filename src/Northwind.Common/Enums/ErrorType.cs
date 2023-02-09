using System.ComponentModel;

namespace Northwind.Common.Enums
{
    public enum ErrorType
    {
        [Description("INVALID_ID")]
        INVALID_ID,

        [Description("INVALID_REQUEST_PARAMETERS")]
        INVALID_REQUEST_PARAMETERS,

        [Description("INSTANCE_NOT_FOUND")]
        INSTANCE_NOT_FOUND,

        [Description("INSTANCE_EXISTS")]
        INSTANCE_EXISTS,

        [Description("INVALID_FILE_EXTENSION")]
        INVALID_FILE_EXTENSION,

        [Description("INVALID_DATE_FORMAT")]
        INVALID_DATE_FORMAT,

        [Description("EMPTY_FILE")]
        EMPTY_FILE,

        [Description("OPERATIONAL_EXCEPTION")]
        OPERATIONAL_EXCEPTION,

        [Description("INVALID_OPERATION")]
        INVALID_OPERATION,

        [Description("SERVER_INTERNAL_ERROR")]
        SERVER_INTERNAL_ERROR,
    }
}
