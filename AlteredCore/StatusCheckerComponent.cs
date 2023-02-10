using System;
using System.Runtime.Serialization;
using Eco.Gameplay.Objects;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using Eco.Gameplay.Components;

namespace Eco.AlteredCore
{
    [Serialized]
    public class StatusCheckerComponent : WorldObjectComponent
    {
        private StatusElement status;
        private bool enabled = true;

        public override void Initialize()
        {
            base.Initialize();
            this.status = this.Parent.GetComponent<StatusComponent>().CreateStatusElement();
            this.enabled = this.EnabledTest(this.Parent.Position.XYZi());
            this.UpdateStatus();
        }

        public void UpdateStatus() => this.status.SetStatusMessage(this.Enabled, this.SuccessStatusMessage, this.FailStatusMessage);

        public override bool Enabled => this.enabled;

        public Func<Vector3i, bool> EnabledTest { get; set; } = (Func<Vector3i, bool>)(x => true);

        public LocString FailStatusMessage { get; set; } = Localizer.DoStr("No Success.");
        public LocString SuccessStatusMessage { get; set; } = Localizer.DoStr("Great Success.");

        public void UpdateEnabled() => this.enabled = this.EnabledTest(this.Parent.Position.XYZi());
    }
}