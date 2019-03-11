using System;
using System.IO;

namespace Pocket.Common
{
  public static class DirectoryInfoExtensions
  {
    #region Copy
    
    public struct CopyExpression
    {
      private readonly DirectoryInfo _root;

      public CopyExpression(DirectoryInfo root)
      {
        _root = root;
      }

      public CopyDirectoriesExpression Where(Predicate<FileInfo> file) =>
        new CopyDirectoriesExpression(_root, file);
      public CopyDirectoriesExpression Where(Predicate<DirectoryInfo> directory) =>
        new CopyDirectoriesExpression(_root, null, directory);

      public CopyFilesExpression Files => new CopyFilesExpression(_root);
      
      public void To(string path, string mask = "*.*")
      {
        new CopyDirectoriesExpression(_root)
          .To(path, mask);
      }
    }
    
    public struct CopyFilesExpression
    {
      private readonly DirectoryInfo _root;
      private readonly Predicate<FileInfo> _file;

      public CopyFilesExpression(DirectoryInfo root, Predicate<FileInfo> file = null)
      {
        _root = root;
        _file = file;
      }

      public CopyFilesExpression Where(Predicate<FileInfo> file) =>
        new CopyFilesExpression(_root, _file == null ? file : _file + file);
      
      public void To(string path, string mask = "*.*")
      {
        if (!Directory.Exists(path))
          path.AsDirectory().Create();

        foreach (var x in _root.GetFiles(mask))
        {
          var isOk = (_file?.Invoke(x)).Or(true);
          if (isOk)
            x.CopyTo($"{path}\\{x.Name}", overwrite: true);
        }
      }
    }

    public struct CopyDirectoriesExpression
    {
      private readonly DirectoryInfo _root;
      private readonly Predicate<FileInfo> _file;
      private readonly Predicate<DirectoryInfo> _directory;

      public CopyDirectoriesExpression(DirectoryInfo root, Predicate<FileInfo> file = null, Predicate<DirectoryInfo> directory = null)
      {
        _root = root;
        _file = file;
        _directory = directory;
      }
      
      public CopyDirectoriesExpression Where(Predicate<FileInfo> file) =>
        new CopyDirectoriesExpression(_root, _file == null ? file : _file + file);
      public CopyDirectoriesExpression Where(Predicate<DirectoryInfo> directory) =>
        new CopyDirectoriesExpression(_root, _file, _directory == null ? directory : _directory + directory);

      public void To(string path, string mask = "*.*")
      {
        new CopyFilesExpression(_root, _file).To(path, mask);

        foreach (var x in _root.GetDirectories())
        {
          var isOk = (_directory?.Invoke(x)).Or(true); 
          if (isOk)
            x.Copy()
              .Where(_file)
              .Where(_directory)
              .To($"{path}\\{x.Name}");
        }
      }
    }
    
    public static CopyExpression Copy(this DirectoryInfo self) =>
      new CopyExpression(self);
    
    #endregion

    #region Clear

    public struct ClearExpression
    {
      private readonly DirectoryInfo _root;
      private readonly Predicate<FileInfo> _file;
      private readonly Predicate<DirectoryInfo> _directory;

      public ClearExpression(DirectoryInfo root, Predicate<FileInfo> file = null, Predicate<DirectoryInfo> directory = null)
      {
        _root = root;
        _file = file;
        _directory = directory;
      }

      public void Files()
      {
        if (!_root.Exists)
          return;
        
        foreach (var x in _root.GetFiles())
        {
          var isOk = (_file?.Invoke(x)).Or(true);
          if (isOk)
            x.Delete();
        }
      }
      
      public void Directories()
      {
        if (!_root.Exists)
          return;
        
        foreach (var x in _root.GetDirectories())
        {
          var isOk = (_directory?.Invoke(x)).Or(true);
          if (isOk)
          {
            x.Clear().Directories();
            x.Delete();
          }
        }
      }
      
      public void All()
      {
        Files();
        Directories();
      }
    }

    public static ClearExpression Clear(this DirectoryInfo self) =>
      new ClearExpression(self);

    #endregion
  }
}