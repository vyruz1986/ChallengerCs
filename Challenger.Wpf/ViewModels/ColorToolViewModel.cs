﻿using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;

using Challenger.Wpf.Extensions;

using MaterialDesignColors;

using MaterialDesignThemes.Wpf;

using Prism.Commands;
using Prism.Mvvm;

namespace Challenger.Wpf.ViewModels
{
    public class ColorToolViewModel : BindableBase
    {
        private readonly PaletteHelper _paletteHelper = new PaletteHelper();

        private ColorScheme _activeScheme;
        private Color? _selectedColor;

        private Color? _primaryColor;
        private Color? _secondaryColor;
        private Color? _primaryForegroundColor;
        private Color? _secondaryForegroundColor;

        public ColorToolViewModel()
        {
            ToggleBaseCommand = new DelegateCommand<bool?>(ApplyBase);
            ChangeHueCommand = new DelegateCommand<Color?>(ChangeHue);
            ChangeCustomHueCommand = new DelegateCommand<Color?>(ChangeCustomColor);
            ChangeToPrimaryCommand = new DelegateCommand(() => ChangeScheme(ColorScheme.Primary));
            ChangeToSecondaryCommand = new DelegateCommand(() => ChangeScheme(ColorScheme.Secondary));
            ChangeToPrimaryForegroundCommand = new DelegateCommand(() => ChangeScheme(ColorScheme.PrimaryForeground));
            ChangeToSecondaryForegroundCommand = new DelegateCommand(() => ChangeScheme(ColorScheme.SecondaryForeground));

            var theme = _paletteHelper.GetTheme();

            _primaryColor = theme.PrimaryMid.Color;
            _secondaryColor = theme.SecondaryMid.Color;

            SelectedColor = _primaryColor;
        }

        public ColorScheme ActiveScheme
        {
            get => _activeScheme;
            set => SetProperty(ref _activeScheme, value);
        }

        public Color? SelectedColor
        {
            get => _selectedColor;
            set
            {
                if (!SetProperty(ref _selectedColor, value))
                    return;

                if (value is Color color)
                    ChangeCustomColor(color);
            }
        }

        public IEnumerable<ISwatch> Swatches { get; } = SwatchHelper.Swatches;

        public ICommand ChangeCustomHueCommand { get; }

        public ICommand ChangeHueCommand { get; }
        public ICommand ChangeToPrimaryCommand { get; }
        public ICommand ChangeToSecondaryCommand { get; }
        public ICommand ChangeToPrimaryForegroundCommand { get; }
        public ICommand ChangeToSecondaryForegroundCommand { get; }

        public ICommand ToggleBaseCommand { get; }

        private void ApplyBase(bool? isDark)
        {
            if (isDark == null)
                return;

            var theme = _paletteHelper.GetTheme();
            var baseTheme = isDark.Value
                ? new MaterialDesignDarkTheme()
                : (IBaseTheme)new MaterialDesignLightTheme();

            theme.SetBaseTheme(baseTheme);
            _paletteHelper.SetTheme(theme);
        }

        private void ChangeCustomColor(Color? color)
        {
            if (color == null)
                return;

            if (ActiveScheme == ColorScheme.Primary)
            {
                _paletteHelper.ChangePrimaryColor(color.Value);
                _primaryColor = color;
            }
            else if (ActiveScheme == ColorScheme.Secondary)
            {
                _paletteHelper.ChangeSecondaryColor(color.Value);
                _secondaryColor = color;
            }
            else if (ActiveScheme == ColorScheme.PrimaryForeground)
            {
                SetPrimaryForegroundToSingleColor(color.Value);
                _primaryForegroundColor = color;
            }
            else if (ActiveScheme == ColorScheme.SecondaryForeground)
            {
                SetSecondaryForegroundToSingleColor(color.Value);
                _secondaryForegroundColor = color;
            }
        }

        private void ChangeScheme(ColorScheme scheme)
        {
            ActiveScheme = scheme;
            if (ActiveScheme == ColorScheme.Primary)
            {
                SelectedColor = _primaryColor;
            }
            else if (ActiveScheme == ColorScheme.Secondary)
            {
                SelectedColor = _secondaryColor;
            }
            else if (ActiveScheme == ColorScheme.PrimaryForeground)
            {
                SelectedColor = _primaryForegroundColor;
            }
            else if (ActiveScheme == ColorScheme.SecondaryForeground)
            {
                SelectedColor = _secondaryForegroundColor;
            }
        }

        private void ChangeHue(Color? hue)
        {
            if (hue == null)
                return;

            SelectedColor = hue;
            if (ActiveScheme == ColorScheme.Primary)
            {
                _paletteHelper.ChangePrimaryColor(hue.Value);
                _primaryColor = hue;
            }
            else if (ActiveScheme == ColorScheme.Secondary)
            {
                _paletteHelper.ChangeSecondaryColor(hue.Value);
                _secondaryColor = hue;
            }
            else if (ActiveScheme == ColorScheme.PrimaryForeground)
            {
                SetPrimaryForegroundToSingleColor(hue.Value);
                _primaryForegroundColor = hue;
            }
            else if (ActiveScheme == ColorScheme.SecondaryForeground)
            {
                SetSecondaryForegroundToSingleColor(hue.Value);
                _secondaryForegroundColor = hue;
            }
        }

        private void SetPrimaryForegroundToSingleColor(Color color)
        {
            var theme = _paletteHelper.GetTheme();

            theme.PrimaryLight = new ColorPair(theme.PrimaryLight.Color, color);
            theme.PrimaryMid = new ColorPair(theme.PrimaryMid.Color, color);
            theme.PrimaryDark = new ColorPair(theme.PrimaryDark.Color, color);

            _paletteHelper.SetTheme(theme);
        }

        private void SetSecondaryForegroundToSingleColor(Color color)
        {
            var theme = _paletteHelper.GetTheme();

            theme.SecondaryLight = new ColorPair(theme.SecondaryLight.Color, color);
            theme.SecondaryMid = new ColorPair(theme.SecondaryMid.Color, color);
            theme.SecondaryDark = new ColorPair(theme.SecondaryDark.Color, color);

            _paletteHelper.SetTheme(theme);
        }
    }
}
