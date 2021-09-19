﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dawn;

namespace Presentation.MenuBuilder
{
    public class MenuBuilder
    {
        private const string DefaultTitle      = "Menu";
        private const string DefaultExitOption = "Salir";

        private Menu Menu { get; }

        public MenuBuilder(Menu menu)
        {
            Menu            = menu;
            Menu.Title      = DefaultTitle;
            Menu.ExitOption = DefaultExitOption;
        }

        public MenuBuilder WithTitle(string title)
        {
            Menu.Title = string.IsNullOrEmpty(title) ? DefaultTitle : title;
            return this;
        }

        public MenuBuilder WithExitOption(string title)
        {
            Menu.ExitOption = string.IsNullOrEmpty(title) ? DefaultTitle : title;
            return this;
        }

        public MenuBuilder WithOptions(IEnumerable<string> options)
        {
            IEnumerable<string> safeOptions =
                Guard.Argument(options, nameof(options)).DoesNotContainNull().Value;

            Menu.Options = safeOptions;
            return this;
        }

        public MenuBuilder WithAsyncActions(IEnumerable<Func<CancellationToken, Task>> actions)
        {
            Menu.AsyncActions = Guard.Argument(actions, nameof(actions)).DoesNotContainNull().Value;
            return this;
        }

        public MenuBuilder WithClear()
        {
            Menu.ClearConsole = true;
            return this;
        }

        public MenuBuilder WithQuestion(string question)
        {
            Menu.Question = $"\n{question}";
            return this;
        }

        public Menu Build()
        {
            if (!Menu.Options.Any())
            {
                Menu.AddAsyncOption("", Menu.PassAsync);
                Menu.AddAsyncOption(DefaultExitOption, Menu.ExitAsync);
            }

            return Menu;
        }
    }
}