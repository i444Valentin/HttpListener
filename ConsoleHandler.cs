using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace HttpListenerProject
{
    class ConsoleHandler
    {

        static bool exitSystem = false;
        static int ctrlCCount;

        #region Trap application termination
        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);

        private delegate bool EventHandler(CtrlType sig);
        static EventHandler _handler;

        enum CtrlType
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }

        private static bool Handler(CtrlType sig)
        {
            //do your cleanup here


            //allow main to run off
            exitSystem = true;
            if (ctrlCCount == 1)
            {
                Console.WriteLine("Сервер остановлен");
                Environment.Exit(0);
            }
            else Console.WriteLine("Для завершения работы нажмите Ctrl+C еще раз");

            ctrlCCount++;

            return true;
        }

        protected static void InitializeEventHandlers()
        {
            // реакция на событие закрытия окна, CTRL-C, kill и т.д
            _handler += new EventHandler(Handler);
            SetConsoleCtrlHandler(_handler, true);
        }
        #endregion
    }
}
