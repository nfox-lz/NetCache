using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Compete.NetCache.Logging
{
    /// <summary>
    /// 日志。
    /// </summary>
    public sealed class ElLogger : ILogger, ICloneable
    {
        /// <summary>
        /// 日志入口。
        /// </summary>
        private LogEntry logEntry;

        /// <summary>
        /// 是否同时输出到控制台。
        /// </summary>
        public bool ConsoleOutput { get; set; }

        /// <summary>
        /// 是否输出抬头项。
        /// </summary>
        public bool ConsoleOutputTitle { get; set; }

        public object Clone()
        {
            return new ElLogger(logEntry.Clone() as LogEntry);
        }

        /// <summary>
        /// 初始化静态成员。
        /// </summary>
        public ElLogger()
        {
            ConsoleOutput = false;
            ConsoleOutputTitle = true;

            logEntry = new LogEntry();
        }

        public ElLogger(LogEntry entry)
        {
            ConsoleOutput = false;
            ConsoleOutputTitle = true;

            logEntry = entry;
        }

        /// <summary>
        /// 写消息。
        /// </summary>
        public void Write()
        {
            try
            {
                Logger.Write(logEntry);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
#if DEBUG
                System.Diagnostics.Debug.WriteLine(exception);
#endif
            }
            if (ConsoleOutput)
            {//输出到控制台。
                if (ConsoleOutputTitle)
                {//输出抬头项。
                    Console.WriteLine("=====================================Begin=====================================");
                    Console.WriteLine("Title          : " + logEntry.Title);
                    Console.WriteLine("-------------------------------------------------------------------------------");
                    Console.WriteLine("Machine name   : " + logEntry.MachineName);
                    Console.WriteLine("AppDomain name : " + logEntry.AppDomainName);
                    Console.WriteLine("Process id     : " + logEntry.ProcessId);
                    Console.WriteLine("Process name   : " + logEntry.ProcessName);
                    Console.WriteLine("Thread id      : " + logEntry.Win32ThreadId);
                    Console.WriteLine("Time           : " + DateTime.Now.ToString());
                    Console.WriteLine("Severity       : " + logEntry.Severity);
                    Console.WriteLine("-------------------------------------------------------------------------------");
                }
                Console.WriteLine("Message        : " + logEntry.Message);
                if (ConsoleOutputTitle)//输出抬头项。
                    Console.WriteLine("======================================End======================================");
            }
        }

        /// <summary>
        /// 写消息。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="title">标题。</param>
        /// <param name="severity">事件的类型。</param>
        /// <param name="rgs">附加信息。</param>
        public void Write(string message, string title, TraceEventType severity, int priority, params object[] rgs)
        {
            logEntry.Message = string.Format(message, rgs);
            logEntry.Severity = severity;
            logEntry.Title = title;
            logEntry.Priority = priority;
            Write();
        }

        /// <summary>
        /// 写消息，事件的类型为“信息（Information）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="title">标题。</param>
        /// <param name="rgs">附加信息。</param>
        public void Write(string message, string title, params object[] rgs)
        {
            Write(message, title, TraceEventType.Information, 2, rgs);
        }

        /// <summary>
        /// 写消息。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="severity">事件的类型。</param>
        /// <param name="rgs">附加信息。</param>
        public void Write(string message, TraceEventType severity, int priority, params object[] rgs)
        {
            Write(message, severity.ToString(), severity, priority, rgs);
        }

        /// <summary>
        /// 写消息，事件的类型为“信息（Information）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="rgs">附加信息。</param>
        public void Write(string message, params object[] rgs)
        {
            Write(message, TraceEventType.Information, 2, rgs);
        }

        /// <summary>
        /// 写消息，事件的类型为“调试跟踪（Verbose）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="rgs">附加信息。</param>
        public void Verbose(string message, params object[] rgs)
        {
            Write(message, TraceEventType.Verbose , 0, rgs);
        }
        /// <summary>
        /// 写消息，事件的类型为“调试跟踪（Verbose）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="title">标题。</param>
        /// <param name="rgs">附加信息。</param>
        public void Verbose(string message, string title, params object[] rgs)
        {
            Write(message, title, TraceEventType.Verbose, 0, rgs);
        }

        /// <summary>
        /// 写消息，事件的类型为“调试（Debug）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="rgs">附加信息。</param>
        public void Debug(string message, params object[] rgs)
        {
            Write(message, TraceEventType.Verbose, 0, rgs);
        }
        /// <summary>
        /// 写消息，事件的类型为“调试（Debug）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="title">标题。</param>
        /// <param name="rgs">附加信息。</param>
        public void Debug(string message, string title, params object[] rgs)
        {
            Write(message, title, TraceEventType.Verbose, 0, rgs);
        }

        /// <summary>
        /// 写消息，事件的类型为“信息（Information）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="rgs">附加信息。</param>
        public void Information(string message, params object[] rgs)
        {
            Write(message, TraceEventType.Information, 2, rgs);
        }
        /// <summary>
        /// 写消息，事件的类型为“信息（Information）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="title">标题。</param>
        /// <param name="rgs">附加信息。</param>
        public void Information(string message, string title, params object[] rgs)
        {
            Write(message, title, TraceEventType.Information, 2, rgs);
        }

        /// <summary>
        /// 写消息，事件的类型为“非关键性问题 警告（Warning）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="rgs">附加信息。</param>
        public void Warning(string message, params object[] rgs)
        {
            Write(message, TraceEventType.Warning, 3, rgs);
        }
        /// <summary>
        /// 写消息，事件的类型为“非关键性问题 警告（Warning）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="title">标题。</param>
        /// <param name="rgs">附加信息。</param>
        public void Warning(string message, string title, params object[] rgs)
        {
            Write(message, title, TraceEventType.Warning, 3, rgs);
        }

        /// <summary>
        /// 写消息，事件的类型为“可恢复的错误 错误（Error）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="rgs">附加信息。</param>
        public void Error(string message, params object[] rgs)
        {
            Write(message, TraceEventType.Error, 4, rgs);
        }
        /// <summary>
        /// 写消息，事件的类型为“可恢复的错误 错误（Error）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="title">标题。</param>
        /// <param name="rgs">附加信息。</param>
        public void Error(string message, string title, params object[] rgs)
        {
            Write(message, title, TraceEventType.Error, 4, rgs);
        }

        /// <summary>
        /// 将异常输出到日志。
        /// </summary>
        /// <remarks>
        /// 异常会按照Error进行输出，并根据Debug版或Release版决定是只输出消息信息还是全部输出。全部输出时，包括调用堆栈等信息。
        /// </remarks>
        /// <param name="exception">异常对象。</param>
        public void Error(CompilerError error)
        {
#if DEBUG
            Error(error.ToString());
#else
            Error(error.ErrorText);
#endif
        }

        /// <summary>
        /// 将异常输出到日志。
        /// </summary>
        /// <remarks>
        /// 异常会按照Error进行输出，并根据Debug版或Release版决定是只输出消息信息还是全部输出。全部输出时，包括调用堆栈等信息。
        /// </remarks>
        /// <param name="exception">异常对象。</param>
        public void LogException(Exception exception)
        {
#if DEBUG
            Error(exception.ToString());
#else
            Error(exception.Message);
#endif
        }

        /// <summary>
        /// 写消息，事件的类型为“错误或应用程序崩溃（Critical）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="rgs">附加信息。</param>
        public void Critical(string message, params object[] rgs)
        {
            Write(message, TraceEventType.Critical, 5, rgs);
        }
        /// <summary>
        /// 写消息，事件的类型为“错误或应用程序崩溃（Critical）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="title">标题。</param>
        /// <param name="rgs">附加信息。</param>
        public void Critical(string message, string title, params object[] rgs)
        {
            Write(message, title, TraceEventType.Critical, 5, rgs);
        }

        /// <summary>
        /// 写消息，事件的类型为“逻辑操作的开始（Start）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="rgs">附加信息。</param>
        public void Start(string message, params object[] rgs)
        {
            Write(message, TraceEventType.Start, 1, rgs);
        }
        /// <summary>
        /// 写消息，事件的类型为“逻辑操作的开始（Start）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="title">标题。</param>
        /// <param name="rgs">附加信息。</param>
        public void Start(string message, string title, params object[] rgs)
        {
            Write(message, title, TraceEventType.Start, 1, rgs);
        }

        /// <summary>
        /// 写消息，事件的类型为“逻辑操作的停止（Stop）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="rgs">附加信息。</param>
        public void Stop(string message, params object[] rgs)
        {
            Write(message, TraceEventType.Stop, 1, rgs);
        }
        /// <summary>
        /// 写消息，事件的类型为“逻辑操作的停止（Stop）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="title">标题。</param>
        /// <param name="rgs">附加信息。</param>
        public void Stop(string message, string title, params object[] rgs)
        {
            Write(message, title, TraceEventType.Stop, 1, rgs);
        }

        /// <summary>
        /// 写消息，事件的类型为“逻辑操作的挂起（Suspend）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="rgs">附加信息。</param>
        public void Suspend(string message, params object[] rgs)
        {
            Write(message, TraceEventType.Suspend, 1, rgs);
        }
        /// <summary>
        /// 写消息，事件的类型为“逻辑操作的挂起（Suspend）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="title">标题。</param>
        /// <param name="rgs">附加信息。</param>
        public void Suspend(string message, string title, params object[] rgs)
        {
            Write(message, title, TraceEventType.Suspend, 1, rgs);
        }

        /// <summary>
        /// 写消息，事件的类型为“逻辑操作的恢复（Resume）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="rgs">附加信息。</param>
        public void Resume(string message, params object[] rgs)
        {
            Write(message, TraceEventType.Resume, 1, rgs);
        }
        /// <summary>
        /// 写消息，事件的类型为“逻辑操作的恢复（Resume）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="title">标题。</param>
        /// <param name="rgs">附加信息。</param>
        public void Resume(string message, string title, params object[] rgs)
        {
            Write(message, title, TraceEventType.Resume, 1, rgs);
        }

        /// <summary>
        /// 写消息，事件的类型为“相关标识的更改（Transfer）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="rgs">附加信息。</param>
        public void Transfer(string message, params object[] rgs)
        {
            Write(message, TraceEventType.Transfer, 1, rgs);
        }

        /// <summary>
        /// 写消息，事件的类型为“相关标识的更改（Transfer）”。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="title">标题。</param>
        /// <param name="rgs">附加信息。</param>
        public void Transfer(string message, string title, params object[] rgs)
        {
            Write(message, title, TraceEventType.Transfer, 1, rgs);
        }
    }
}
