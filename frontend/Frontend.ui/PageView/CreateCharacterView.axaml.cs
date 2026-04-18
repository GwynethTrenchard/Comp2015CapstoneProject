using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Frontend.ui;

public partial class CreateCharacterView : UserControl
{
    public CreateCharacterView()
    {
        InitializeComponent();
    }

    private void OnReturnClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new CampaignView());
    }

    private void OnSaveCharacterClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new CampaignView());
    }

    private int CalculateProficiency(int level)
    {
        return (int)Math.Floor((level - 1) / 4.0) + 2;
    }

    private void OnLevelChanged(object? sender, TextChangedEventArgs e)
    {
        if (int.TryParse(LevelBox.Text, out int level))
        {
            int prof = CalculateProficiency(level);
            ProfBonusText.Text = $"+{prof}";
        }
        else
        {
            ProfBonusText.Text = "+0";
        }
    }

    private void OnAbilityChanged(object? sender, TextChangedEventArgs e)
    {
        var box = sender as TextBox;
        if (box == null) return;

        if (!int.TryParse(box.Text, out int value))
            return;

        int mod = (int)Math.Floor((value - 10) / 2.0);
        string formatted = mod >= 0 ? $"+{mod}" : mod.ToString();

        switch (box.Name)
        {
            case "StrengthBox":
                StrengthMod.Text = formatted;
                break;

            case "DexterityBox":
                DexterityMod.Text = formatted;
                break;

            case "ConstitutionBox":
                ConstitutionMod.Text = formatted;
                break;

            case "IntelligenceBox":
                IntelligenceMod.Text = formatted;
                break;

            case "WisdomBox":
                WisdomMod.Text = formatted;
                break;

            case "CharismaBox":
                CharismaMod.Text = formatted;
                break;
        }
    }

}

