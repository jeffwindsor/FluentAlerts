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

            var dir = Path.GetDirectoryName(filePath);
            var backUpFileName = string.Format("{0}{1}{2}_UTC{3}{4}",
                                               dir,
                                               (string.IsNullOrWhiteSpace(dir))?string.Empty:"\\",
                                               Path.GetFileNameWithoutExtension(filePath),
                                               DateTime.UtcNow.ToIsoFormat(),
                                               Path.GetExtension(filePath));

            System.IO.File.Copy(filePath, backUpFileName);
        }

    }
}
