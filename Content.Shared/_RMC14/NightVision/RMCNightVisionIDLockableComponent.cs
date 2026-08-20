using Content.Shared.Access.Systems;
using Content.Shared.Access.Components;
using Content.Shared._RMC14.Access;
using Robust.Shared.Prototypes;

namespace Content.Shared._RMC14.NightVision;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
[Access(typeof(RMCNightVisionIdLockableSystem))]
public sealed partial class RMCNightVisionIdLockComponent : Component
{
    /// <summary>
    /// Current lock state shown to gameplay and visuals.
    /// </summary>
    [DataField, AutoNetworkedField]
    public bool Locked;

    /// <summary>
    /// Trimmed ID card full name currently bound to this sight.
    /// </summary>
    [DataField, AutoNetworkedField]
    public string? OwnerName;

    /// <summary>
    /// Access tags that can unlock this sight in addition to the owner.
    /// An empty list means the sight has no override path.
    /// </summary>
    [DataField, AutoNetworkedField]
    public List<ProtoId<AccessLevelPrototype>> OverrideAccesses = new();
}
