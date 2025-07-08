using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace xCert.Core.Engines;
public static class IOEngine {
    public static bool Resolve(out string path, params string[] paths) {
        path = Path.GetFullPath(Path.Combine((new string[] { GlobalContext.Directory }).Concat(paths).ToArray()));
        return Path.Exists(path);
    }
    public static bool ResolveRaw(out string path, params string[] paths) {
        path = Path.GetFullPath(Path.Combine(paths));
        return Path.Exists(path);
    }
    public static bool ResolveAny(out string path, params string[] paths) => Resolve(out path, paths) || ResolveRaw(out path, paths);

    public static string Resolve(params string[] paths) { Resolve(out var path, paths); return path; }
    public static string ResolveRaw(params string[] paths) { ResolveRaw(out var path, paths); return path; }
    public static string ResolveAny(params string[] paths) { ResolveAny(out var path, paths); return path; }

    public static string Read(params string[] paths) => Resolve(out var path, paths) ? File.ReadAllText(path) : string.Empty;
    public static string ReadRaw(params string[] paths) => ResolveRaw(out var path, paths) ? File.ReadAllText(path) : string.Empty;
    public static string ReadAny(params string[] paths) => ResolveAny(out var path, paths) ? File.ReadAllText(path) : string.Empty;

    public static bool Read(out string content, params string[] paths) {
        bool found = Resolve(out var path, paths);
        content = found ? File.ReadAllText(path) : string.Empty;
        return found;
    }
    public static bool ReadRaw(out string content, params string[] paths) {
        bool found = ResolveRaw(out var path, paths);
        content = found ? File.ReadAllText(path) : string.Empty;
        return found;
    }
    public static bool ReadAny(out string content, params string[] paths) {
        bool found = ResolveAny(out var path, paths);
        content = found ? File.ReadAllText(path) : string.Empty;
        return found;
    }

    public static bool Check(params string[] paths) => Resolve(out _, paths);
    public static bool CheckRaw(params string[] paths) => ResolveRaw(out _, paths);
    public static bool CheckAny(params string[] paths) => ResolveAny(out _, paths);

    public static IOWriter<string> Write(this string content) => new(content, File.WriteAllText);
    public static IOWriter<byte[]> Write(this byte[] content) => new(content, File.WriteAllBytes);

    public class IOWriter<T> {
        private T? _content { get; init; }
        private Action<string, T> _write;

        public IOWriter(T? content, Action<string, T> write) {
            _content = content;
            _write = write;
        }

        private T To(string path) {
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            if(_content is not null)
                _write(path, _content);

            return _content;
        }

        public T To(params string[] paths) => To(Resolve(paths));

        public T ToRaw(params string[] paths) => To(ResolveRaw(paths));

        public T ToAny(params string[] paths) => To(ResolveAny(paths));
    }
}
