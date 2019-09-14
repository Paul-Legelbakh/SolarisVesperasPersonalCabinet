using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalCabinet.UserInterface
{
    public enum ErrorCode
    {
        OK = 200,
        INFO = 201,
        WARNING = 202,
        CACHE_IS_CLEAR = 211,
        CACHE_IS_NOT_CLEAR = 212,
        NOT_VALID = 213,

        ERROR = 101,
        SERVER_ERROR = 111,
        CLIENT_ERROR = 112,
        PARSE_ERROR = 113,
        CONNECTION_ERROR = 114,

        NOT_EXISTS = 701,
        ALREADY_EXISTS = 702,
        NULL_REFERENCE = 703
    }
}
