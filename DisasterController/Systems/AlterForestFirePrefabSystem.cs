// <copyright file="AlterForestFirePrefabSystem.cs" company="Yenyang's Mods. MIT License">
// Copyright (c) Yenyang's Mods. MIT License. All rights reserved.
// </copyright>

namespace DisasterController.Systems
{
    using Colossal.Entities;
    using Colossal.Logging;
    using Colossal.Serialization.Entities;

    using DisasterController;

    using Game;
    using Game.Prefabs;

    using Unity.Entities;

    /// <summary>
    /// A system for altering the forest fire prefab based on Settings.
    /// </summary>
    public partial class AlterForestFirePrefabSystem : GameSystemBase
    {
        private PrefabID prefabID = new ("EventPrefab", "Forest Fire");
        private PrefabSystem m_PrefabSystem;
        private EntityQuery m_ForestFirePrefabQuery;
        private ILog m_Log;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlterForestFirePrefabSystem"/> class.
        /// </summary>
        public AlterForestFirePrefabSystem()
        {
        }

        /// <inheritdoc/>
        protected override void OnCreate()
        {
            base.OnCreate();

            m_PrefabSystem = World.GetOrCreateSystemManaged<PrefabSystem>();
            m_Log = DisasterControllerMod.Instance.Logger;
            m_Log.Info($"{nameof(AlterForestFirePrefabSystem)} created!");
            Enabled = false;
        }

        /// <inheritdoc/>
        protected override void OnUpdate()
        {
            m_ForestFirePrefabQuery = SystemAPI.QueryBuilder()
                .WithAllRW<FireData>()
                .WithAll<EventData>()
                .Build();

            RequireForUpdate(m_ForestFirePrefabQuery);
            if (m_ForestFirePrefabQuery.IsEmptyIgnoreFilter)
            {
                return;
            }

            if (!m_PrefabSystem.TryGetPrefab(prefabID, out var prefabBase))
            {
                m_Log.Info($"{nameof(AlterForestFirePrefabSystem)}.{nameof(OnUpdate)} couldn't find prefab base. ");
                return;
            }

            m_Log.Debug($"{nameof(AlterForestFirePrefabSystem)}.{nameof(OnUpdate)} prefab.name " + prefabBase.name);

            if (!m_PrefabSystem.TryGetEntity(prefabBase, out var prefabEntity))
            {
                m_Log.Info($"{nameof(AlterForestFirePrefabSystem)}.{nameof(OnUpdate)} couldn't find prefab entity. ");
                return;
            }

            m_Log.Debug($"{nameof(AlterForestFirePrefabSystem)}.{nameof(OnUpdate)} prefabEntity.Index:Version {prefabEntity.Index}:{prefabEntity.Version}");

            if (!EntityManager.TryGetComponent<FireData>(prefabEntity, out var fireData))
            {
                m_Log.Info($"{nameof(AlterForestFirePrefabSystem)}.{nameof(OnUpdate)} couldn't find fire data data component. ");
                return;
            }

            fireData.m_StartProbability = DisasterControllerMod.Instance.Settings.StartProbability;
            fireData.m_SpreadProbability = DisasterControllerMod.Instance.Settings.SpreadProbability;
            fireData.m_EscalationRate = DisasterControllerMod.Instance.Settings.EscalationRate;
            fireData.m_SpreadRange = DisasterControllerMod.Instance.Settings.SpreadRange;
            fireData.m_StartIntensity = DisasterControllerMod.Instance.Settings.StartIntensity;

            EntityManager.SetComponentData(prefabEntity, fireData);
            Enabled = false;
        }

        /// <inheritdoc/>
        protected override void OnGameLoadingComplete(Purpose purpose, GameMode mode)
        {
            if (purpose is not Purpose.LoadGame and not Purpose.NewGame || !mode.HasFlag(GameMode.Game))
            {
                return;
            }

            Enabled = true;
        }
    }
}
