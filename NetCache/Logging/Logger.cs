using System;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace Compete.NetCache.Logging
{
    public interface ILogger
    {
        /// <summary>
        /// 是否同时输出到控制台。
        /// </summary>
        bool ConsoleOutput { get; set; }

        /// <summary>
        /// 是否输出抬头项。
        /// </summary>
        bool ConsoleOutputTitle { get; set; }

        /// <summary>
        /// 写消息。
        /// </summary>
        void Write();

        /// <summary>
        /// 写消息。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="title">标题。</param>
        /// <param name="severity">事件的类型。</param>
        /// <param name="rgs">附加信息。</param>
        void Write(string message, string title, TraceEventType severity, int priority, params object[] rgs);

        /// <summary>
        /// 写消息，事件的类型为“信息（Information）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="title">标题。</param>
        /// <param name="rgs">附加信息。</param>
        void Write(string message, string title, params object[] rgs);

        /// <summary>
        /// 写消息。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="severity">事件的类型。</param>
        /// <param name="rgs">附加信息。</param>
        void Write(string message, TraceEventType severity, int priority, params object[] rgs);

        /// <summary>
        /// 写消息，事件的类型为“信息（Information）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="rgs">附加信息。</param>
        void Write(string message, params object[] rgs);

        /// <summary>
        /// 写消息，事件的类型为“调试跟踪（Verbose）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="rgs">附加信息。</param>
        void Verbose(string message, params object[] rgs);

        /// <summary>
        /// 写消息，事件的类型为“调试跟踪（Verbose）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="title">标题。</param>
        /// <param name="rgs">附加信息。</param>
        void Verbose(string message, string title, params object[] rgs);

        /// <summary>
        /// 写消息，事件的类型为“调试（Debug）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="rgs">附加信息。</param>
        void Debug(string message, params object[] rgs);

        /// <summary>
        /// 写消息，事件的类型为“调试（Debug）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="title">标题。</param>
        /// <param name="rgs">附加信息。</param>
        void Debug(string message, string title, params object[] rgs);

        /// <summary>
        /// 写消息，事件的类型为“信息（Information）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="rgs">附加信息。</param>
        void Information(string message, params object[] rgs);

        /// <summary>
        /// 写消息，事件的类型为“信息（Information）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="title">标题。</param>
        /// <param name="rgs">附加信息。</param>
        void Information(string message, string title, params object[] rgs);

        /// <summary>
        /// 写消息，事件的类型为“非关键性问题 警告（Warning）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="rgs">附加信息。</param>
        void Warning(string message, params object[] rgs);

        /// <summary>
        /// 写消息，事件的类型为“非关键性问题 警告（Warning）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="title">标题。</param>
        /// <param name="rgs">附加信息。</param>
        void Warning(string message, string title, params object[] rgs);

        /// <summary>
        /// 写消息，事件的类型为“可恢复的错误 错误（Error）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="rgs">附加信息。</param>
        void Error(string message, params object[] rgs);

        /// <summary>
        /// 写消息，事件的类型为“可恢复的错误 错误（Error）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="title">标题。</param>
        /// <param name="rgs">附加信息。</param>
        void Error(string message, string title, params object[] rgs);

        /// <summary>
        /// 将异常输出到日志。
        /// </summary>
        /// <remarks>
        /// 异常会按照Error进行输出，并根据Debug版或Release版决定是只输出消息信息还是全部输出。全部输出时，包括调用堆栈等信息。
        /// </remarks>
        /// <param name="exception">异常对象。</param>
        void Error(CompilerError error);

        /// <summary>
        /// 将异常输出到日志。
        /// </summary>
        /// <remarks>
        /// 异常会按照Error进行输出，并根据Debug版或Release版决定是只输出消息信息还是全部输出。全部输出时，包括调用堆栈等信息。
        /// </remarks>
        /// <param name="exception">异常对象。</param>
        void LogException(Exception exception);

        /// <summary>
        /// 写消息，事件的类型为“错误或应用程序崩溃（Critical）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="rgs">附加信息。</param>
        void Critical(string message, params object[] rgs);

        /// <summary>
        /// 写消息，事件的类型为“错误或应用程序崩溃（Critical）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="title">标题。</param>
        /// <param name="rgs">附加信息。</param>
        void Critical(string message, string title, params object[] rgs);

        /// <summary>
        /// 写消息，事件的类型为“逻辑操作的开始（Start）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="rgs">附加信息。</param>
        void Start(string message, params object[] rgs);

        /// <summary>
        /// 写消息，事件的类型为“逻辑操作的开始（Start）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="title">标题。</param>
        /// <param name="rgs">附加信息。</param>
        void Start(string message, string title, params object[] rgs);

        /// <summary>
        /// 写消息，事件的类型为“逻辑操作的停止（Stop）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="rgs">附加信息。</param>
        void Stop(string message, params object[] rgs);

        /// <summary>
        /// 写消息，事件的类型为“逻辑操作的停止（Stop）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="title">标题。</param>
        /// <param name="rgs">附加信息。</param>
        void Stop(string message, string title, params object[] rgs);

        /// <summary>
        /// 写消息，事件的类型为“逻辑操作的挂起（Suspend）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="rgs">附加信息。</param>
        void Suspend(string message, params object[] rgs);

        /// <summary>
        /// 写消息，事件的类型为“逻辑操作的挂起（Suspend）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="title">标题。</param>
        /// <param name="rgs">附加信息。</param>
        void Suspend(string message, string title, params object[] rgs);

        /// <summary>
        /// 写消息，事件的类型为“逻辑操作的恢复（Resume）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="rgs">附加信息。</param>
        void Resume(string message, params object[] rgs);

        /// <summary>
        /// 写消息，事件的类型为“逻辑操作的恢复（Resume）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="title">标题。</param>
        /// <param name="rgs">附加信息。</param>
        void Resume(string message, string title, params object[] rgs);

        /// <summary>
        /// 写消息，事件的类型为“相关标识的更改（Transfer）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="rgs">附加信息。</param>
        void Transfer(string message, params object[] rgs);

        /// <summary>
        /// 写消息，事件的类型为“相关标识的更改（Transfer）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="title">标题。</param>
        /// <param name="rgs">附加信息。</param>
        void Transfer(string message, string title, params object[] rgs);
    }
}
