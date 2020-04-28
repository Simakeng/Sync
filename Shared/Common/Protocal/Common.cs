using System;
using System.Collections.Generic;
using System.Text;

namespace Sync.Common.Protocal
{
    enum Command
    {
        Connection,
        Authorization,
        QueryDirectoryInfo,
        QueryFileInfo,
        QueryDirectoryStructure,
        DownloadFileContent,
        UploadFileContent,
        RequestFile,
        StopRequestFile,
        SendFile,
        StopSendFile,
    }

    enum Info
    {
        FileChanged,
        FileDeleted,
        FileCreated,
        DirectoryCreated,
        DirectoryDeleted,
    }

    enum PacketType
    {
        Ping = 0,
        Pong = 1,
        Command = 2,
        Info = 3,
        Data = 4,
    }


}
