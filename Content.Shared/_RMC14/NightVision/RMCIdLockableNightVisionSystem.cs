using Content.Shared.Access.Components;
using Content.Shared.Access.Systems;
using Content.Shared.Equipment;
using Content.Shared.Interaction;
using Content.Shared.Popups;

namespace Content.Shared._RMC14.NightVision;

/// <summary>
///  The first valid ID to equip an unlocked item registers ownership.
///  Once locked, only the matching ID can use it.
///  Permadeath or override accesses remove ownership.
/// </summary>
public sealed class RMCIdLockableNightVisionSystem : EntitySystem
{
  [Dependency] private readonly AccessReaderSystem _accessReader = default!;
  [Dependency] private readonly SharedIdCardSystem _idCard = default!;
  [Dependency] private readonly SharedPopupSystem _popup = default!;

  public override void Initialize()
  {
    SubscribeLocalEvent<RMCIdLockableNightVisionComponent, InteractUsingEvent>(OnInteractUsing);
  }

  private void OnInteractUsing(Entity<RMCIdLockableNightVisionComponent> ent, ref InteractUsingEvent args)
  {
    
  }
}
