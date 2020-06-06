﻿using System;

using DataConverter.Uwp.Helpers;
using DataConverter.Uwp.Views;

using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace DataConverter.Uwp
{
    /// <summary>
    ///     Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
        /// <summary>
        ///     Initializes the singleton application object. This is the first line of authored
        ///     code executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
        }

        public static CoreDispatcher Dispatcher => CoreApplication.MainView.CoreWindow.Dispatcher;

        /// <summary>
        ///     Invoked when the application is launched normally by the end user. Other entry
        ///     points will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            EnsureWindow(e);
        }

        private void EnsureWindow(LaunchActivatedEventArgs args)
        {
            var rootFrame = GetRootFrame();

            ThemeHelper.Initialize();

            var targetPageType = typeof(ConverterPage);
            var targetPageArguments = string.Empty;

            if (args.Kind == ActivationKind.Launch)
                targetPageArguments = args.Arguments;

            _ = rootFrame.Navigate(targetPageType, targetPageArguments);
            Window.Current.Activate();
        }

        private Frame GetRootFrame()
        {
            Frame rootFrame;
            switch (Window.Current.Content)
            {
                case MainPage rootPage:
                    rootFrame = (Frame)rootPage.FindName("rootFrame");
                    break;

                default:
                    var newRootPage = new MainPage();
                    rootFrame = (Frame)newRootPage.FindName("RootFrame");
                    if (rootFrame == null)
                        throw new Exception("Root frame not found");

                    rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];
                    rootFrame.NavigationFailed += OnNavigationFailed;

                    Window.Current.Content = newRootPage;
                    break;
            }

            return rootFrame;
        }

        /// <summary>Invoked when Navigation to a certain page fails</summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">     Details about the navigation failure</param>
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
            => throw new Exception("Failed to load Page " + e.SourcePageType.FullName);

        /// <summary>
        ///     Invoked when application execution is being suspended. Application state is saved
        ///     without knowing whether the application will be terminated or resumed with the
        ///     contents of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">     Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
