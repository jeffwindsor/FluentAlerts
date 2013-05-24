using System;
using System.IO;

namespace FluentAlerts
{
    internal static class FileExporter
    {
        public static string Export(string filePath, string json)
        {
            //Backup Current File
            BackUpFileIfExists(filePath);

            // Write new contents
            System.IO.File.WriteAllText(filePath, json);

            //Return name of back up file or empty
            return filePath;
        }

        private static void BackUpFileIfExists(string filePath)
        {
            if (!System.IO.File.Exists(filePath)) return;

            var backUpFileName = string.Format("{0}\\{1}_UTC{2}.{3}",
                                               Path.GetDirectoryName(filePath),
                                                Path.GetFileName(filePath),
                                               DateTime.UtcNow.ToIsoFormat(),
                                               Path.GetExtension(filePath));

            System.IO.File.Copy(filePath, backUpFileName);
        }

    }
}
