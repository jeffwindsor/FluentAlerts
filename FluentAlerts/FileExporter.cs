using System;
using System.IO;

namespace FluentAlerts
{
    //HACK: Statics
    internal static class FileExporter
    {
        public static string Export(string filePath, string json)
        {
            //Backup Current File
            BackUpFileIfExists(filePath);

            // Write new contents
            File.WriteAllText(filePath, json);

            //Return name of back up file or empty
            return filePath;
        }

        private static void BackUpFileIfExists(string filePath)
        {
            if (!File.Exists(filePath)) return;

            var dir = Path.GetDirectoryName(filePath);
            var backUpFileName = string.Format("{0}{1}{2}_UTC{3}{4}",
                                               dir,
                                               (string.IsNullOrWhiteSpace(dir))?string.Empty:"\\",
                                               Path.GetFileNameWithoutExtension(filePath),
                                               DateTime.UtcNow.ToIsoFormat(),
                                               Path.GetExtension(filePath));

            File.Copy(filePath, backUpFileName);
        }

    }
}
