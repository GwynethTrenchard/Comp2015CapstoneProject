using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Frontend.ui;

public partial class CharacterView : UserControl
{
    public CharacterView()
    {
        InitializeComponent();
    }

    private void OnReturnClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new CampaignView());
    }

    private void OnFeaturesClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new FeatureView());
    }

    private void OnSpellsClicked(object? sender, RoutedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as MainWindow)?.Navigate(new SpellView());
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

        // update modifier
        if (int.TryParse(box.Text, out int value))
        {
            int mod = (int)Math.Floor((value - 10) / 2.0);

            if (box.Name == "StrengthBox")
            {
                StrengthMod.Text = mod >= 0 ? $"+{mod}" : mod.ToString();
            }
            else if (box.Name == "DexterityBox")
            {
                DexterityMod.Text = mod >= 0 ? $"+{mod}" : mod.ToString();
            }
            else if (box.Name == "ConstitutionBox")
            {
                ConstitutionMod.Text = mod >= 0 ? $"+{mod}" : mod.ToString();
            } 
            else if (box.Name == "IntelligenceBox")
            {
                IntelligenceMod.Text = mod >= 0 ? $"+{mod}" : mod.ToString();
            }
            else if (box.Name == "WisdomBox")
            {
                WisdomMod.Text = mod >= 0 ? $"+{mod}" : mod.ToString();
            }
            else if (box.Name == "CharismaBox")
            {
                CharismaMod.Text = mod >= 0 ? $"+{mod}" : mod.ToString();
            }


        }
    }
}

