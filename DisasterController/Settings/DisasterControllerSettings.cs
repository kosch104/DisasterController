// <copyright file="DisasterControllerSettings.cs" company="Yenyang's Mods. MIT License">
// Copyright (c) Yenyang's Mods. MIT License. All rights reserved.
// </copyright>

namespace DisasterController.Settings
{
    using Colossal.IO.AssetDatabase;
    using DisasterController.Systems;
    using Game.Modding;
    using Game.Settings;
    using Unity.Entities;

    /// <summary>
    /// The mod settings for the Anarchy Mod.
    /// </summary>
    [FileLocation("Mods_Yenyang")]
    [SettingsUITabOrder(KForestFireTab)]
    public class DisasterControllerSettings : ModSetting
    {
        /// <summary>
        /// A tab for Forest Fire Settings.
        /// </summary>
        public const string KForestFireTab = "Forest Fire";

        /// <summary>
        /// Initializes a new instance of the <see cref="DisasterControllerSettings"/> class.
        /// </summary>
        /// <param name="mod">DisasterControllerMod.</param>
        public DisasterControllerSettings(IMod mod)
            : base(mod)
        {
            SetDefaults();
        }

        /// <summary>
        /// Gets or sets the probability that a forest fire will start.
        /// </summary>
        [SettingsUISection(KForestFireTab)]
        [SettingsUISlider(min = 0f, max = 2f, step = 0.1f, unit = "floatSingleFraction", scalarMultiplier = 100f)]
        public float StartProbability { get; set; }

        /// <summary>
        /// Gets or sets the intentity of currently active forest fires.
        /// </summary>
        [SettingsUISection(KForestFireTab)]
        [SettingsUISlider(min = 0f, max = 2f, step = 0.1f, unit = "floatSingleFraction")]
        public float StartIntensity { get; set; }

        /// <summary>
        /// Gets or sets the escalation rate of currently active forest fires.
        /// </summary>
        [SettingsUISection(KForestFireTab)]
        [SettingsUISlider(min = 0f, max = 5f, step = 0.1f, unit = "floatSingleFraction")]
        public float EscalationRate { get; set; }

        /// <summary>
        /// Gets or sets the probability that currently active fire will spread from one tree to the next.
        /// </summary>
        [SettingsUISection(KForestFireTab)]
        [SettingsUISlider(min = 0f, max = 10f, step = 0.1f, unit = "floatSingleFraction")]
        public float SpreadProbability { get; set; }

        /// <summary>
        /// Gets or sets the maximum range that currently active fire will spread from one tree to the next.
        /// </summary>
        [SettingsUISlider(min = 0f, max = 50f, step = 5f, unit = "floatSingleFraction")]
        [SettingsUISection(KForestFireTab)]
        public float SpreadRange { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether: Used to force saving of Modsettings if settings would result in empty Json.
        /// </summary>
        [SettingsUIHidden]
        public bool Contra { get; set; }

        /// <summary>
        /// Sets a value indicating whether: a button for Resetting the settings for the Mod.
        /// </summary>
        [SettingsUIButton]
        [SettingsUIConfirmation]
        [SettingsUISection(KForestFireTab)]
        public bool ResetForestFireSettingsButton
        {
            set
            {
                SetDefaults();
                Contra = false;
            }
        }

        /// <inheritdoc/>
        public override void SetDefaults()
        {
            Contra = true;
            StartProbability = 0.005f;
            StartIntensity = 1f;
            EscalationRate = 1.7f;
            SpreadProbability = 1.5f;
            SpreadRange = 30f;
        }

        /// <inheritdoc/>
        public override void Apply()
        {
            AlterForestFirePrefabSystem alterForestFirePrefabSystem = World.DefaultGameObjectInjectionWorld?.GetOrCreateSystemManaged<AlterForestFirePrefabSystem>();
            alterForestFirePrefabSystem.Enabled = true;
            base.Apply();
        }
    }
}
