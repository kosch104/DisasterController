// <copyright file="DisasterControllerMod.cs" company="Yenyang's Mods. MIT License">
// Copyright (c) Yenyang's Mods. MIT License. All rights reserved.
// </copyright>

namespace DisasterController
{
    using System;
    using System.IO;
    using Colossal.IO.AssetDatabase;
    using Colossal.Logging;
    using DisasterController.Settings;
    using DisasterController.Systems;
    using Game;
    using Game.Modding;
    using Game.SceneFlow;

    /// <summary>
    /// Mod entry point.
    /// </summary>
    public class DisasterControllerMod : IMod
    {
        /// <summary>
        /// Gets the install folder for the mod.
        /// </summary>
        private static string m_modInstallFolder;

        /// <summary>
        /// Gets the static reference to the mod instance.
        /// </summary>
        public static DisasterControllerMod Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the Install Folder for the mod as a string.
        /// </summary>
        public static string ModInstallFolder
        {
            get
            {
                if (m_modInstallFolder is null)
                {
                    var thisFullName = Instance.GetType().Assembly.FullName;
                    ExecutableAsset thisInfo = AssetDatabase.global.GetAsset(SearchFilter<ExecutableAsset>.ByCondition(x => x.definition?.FullName == thisFullName)) ?? throw new Exception("This mod info was not found!!!!");
                    m_modInstallFolder = Path.GetDirectoryName(thisInfo.GetMeta().path);
                }

                return m_modInstallFolder;
            }
        }

        /// <summary>
        ///  Gets or sets the static version of the Anarchy Mod Settings.
        /// </summary>
        internal DisasterControllerSettings Settings { get; set; }

        /// <summary>
        /// Gets ILog for mod.
        /// </summary>
        internal ILog Logger { get; private set; }

        /// <inheritdoc/>
        public void OnLoad(UpdateSystem updateSystem)
        {
            Instance = this;
            Logger = LogManager.GetLogger("DisasterController").SetShowsErrorsInUI(false);
#if VERBOSE
            Logger.effectivenessLevel = Level.Verbose;
#elif DEBUG
            Logger.effectivenessLevel = Level.Debug;
#else
            Logger.effectivenessLevel = Level.Info;
#endif
            Settings = new (this);
            Settings.RegisterInOptionsUI();
            AssetDatabase.global.LoadSettings(nameof(DisasterControllerMod), Settings, new DisasterControllerSettings(this));
            Settings.Contra = false;
            Logger.Info("Loading. . .");
            Logger.Info("Handling create world");
            Logger.Info("ModInstallFolder = " + ModInstallFolder);
            GameManager.instance.localizationManager.AddSource("en-US", new LocaleEN(Settings));
            updateSystem.UpdateAt<AlterForestFirePrefabSystem>(SystemUpdatePhase.LateUpdate);
        }

        /// <inheritdoc/>
        public void OnDispose()
        {
            Logger.Info("Disposing..");
            if (Settings != null)
            {
                Settings.UnregisterInOptionsUI();
                Settings = null;
            }
        }

    }
}
