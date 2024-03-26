// <copyright file="LocaleEN.cs" company="Yenyang's Mods. MIT License">
// Copyright (c) Yenyang's Mods. MIT License. All rights reserved.
// </copyright>

namespace DisasterController.Settings
{
    using System.Collections.Generic;
    using Colossal;
    using DisasterController;
    using Game.Settings;

    /// <summary>
    /// Localization for <see cref="DisasterControllerMod"/> in English.
    /// </summary>
    public class LocaleEN : IDictionarySource
    {
        private readonly DisasterControllerSettings m_Setting;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocaleEN"/> class.
        /// </summary>
        /// <param name="setting">Settings class.</param>
        public LocaleEN(DisasterControllerSettings setting)
        {
            m_Setting = setting;
        }

        /// <inheritdoc/>
        public IEnumerable<KeyValuePair<string, string>> ReadEntries(IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                { m_Setting.GetSettingsLocaleID(), "Disaster Controller" },
                { m_Setting.GetOptionTabLocaleID(DisasterControllerSettings.KForestFireTab), "Forest Fires" },
                { m_Setting.GetOptionLabelLocaleID(nameof(DisasterControllerSettings.StartProbability)), "Start Probability x 100" },
                { m_Setting.GetOptionDescLocaleID(nameof(DisasterControllerSettings.StartProbability)), "The likelyhood that any wildtree will catch fire in percent. Actual probably is Value X 0.01." },
                { m_Setting.GetOptionLabelLocaleID(nameof(DisasterControllerSettings.StartIntensity)), "Start Intensity" },
                { m_Setting.GetOptionDescLocaleID(nameof(DisasterControllerSettings.StartIntensity)), "The starting destructive power of the wildfire." },
                { m_Setting.GetOptionLabelLocaleID(nameof(DisasterControllerSettings.EscalationRate)), "Esclation Rate" },
                { m_Setting.GetOptionDescLocaleID(nameof(DisasterControllerSettings.EscalationRate)), "The speed at which the destructive power of the wildfire increases." },
                { m_Setting.GetOptionLabelLocaleID(nameof(DisasterControllerSettings.SpreadProbability)), "Spread Probability" },
                { m_Setting.GetOptionDescLocaleID(nameof(DisasterControllerSettings.SpreadProbability)), "The likelyhood that fire will spread to something else within range." },
                { m_Setting.GetOptionLabelLocaleID(nameof(DisasterControllerSettings.SpreadRange)), "Spread Range" },
                { m_Setting.GetOptionDescLocaleID(nameof(DisasterControllerSettings.SpreadRange)), "When fire is spreading this is the maximum range it can reach another object." },
                { m_Setting.GetOptionLabelLocaleID(nameof(DisasterControllerSettings.ResetForestFireSettingsButton)), "Reset Forest Fire Settings" },
                { m_Setting.GetOptionDescLocaleID(nameof(DisasterControllerSettings.ResetForestFireSettingsButton)), "On confirmation, resets Forest Fire Settings." },
                { m_Setting.GetOptionWarningLocaleID(nameof(DisasterControllerSettings.ResetForestFireSettingsButton)), "Reset Forest Fire Settings?" },
            };
        }

        /// <inheritdoc/>
        public void Unload()
        {
        }
    }
}
