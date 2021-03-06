//          Copyright Jens Granlund 2011.
//      Distributed under the New BSD License.
//     (See accompanying file notice.txt or at 
// http://www.opensource.org/licenses/bsd-license.php)
using System;
using CrcStudio.Messages;
using CrcStudio.Project;

namespace CrcStudio.BuildProcess
{
    public class SmaliHandler : SmaliBaksmaliBase, IFileHandler
    {
        public SmaliHandler(SolutionProperties properties) : base(properties)
        {
        }

        #region IFileHandler Members

        public void ProcessFile(object fileObject, Func<bool> isCanceled)
        {
            if (fileObject == null) throw new ArgumentNullException("fileObject");
            var file = fileObject as CompositFile;
            if (file == null)
                throw new Exception(string.Format("{0} can not handle object of type {1}", GetType().Name,
                                                  fileObject.GetType().Name));

            if (!file.IsDeCompiled) return;
            MessageEngine.AddInformation(this, string.Format("Recompiling classes for {0}", file.Name));

            Recompile(file.FileSystemPath, file.ClassesFolder, file.Project);
            file.Recompiled();
        }

        public bool CanProcess(object fileObject)
        {
            if (!_canRecompile) return false;
            var file = fileObject as CompositFile;
            return file != null;
        }

        #endregion
    }
}