using Robust.Shared.GameStates;
using Robust.Shared.Utility;

namespace Content.Shared._RMC14.Weapons.Ranged.Ammo;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class ProjectileSpriteOverrideComponent : Component
{
    [DataField, AutoNetworkedField]
    public SpriteSpecifier? Sprite;
}
