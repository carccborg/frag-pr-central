using Content.Shared.Access.Components;
using Content.Shared.Access.Systems;
using Content.Shared.Inventory.Events;
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
    SubscribeLocalEvent<RMCIdLockableNightVisionComponent, BeingEquippedAttemptEvent>(OnBeingEquipped);
    SubscribeLocalEvent<RMCIdLockableNightVisionComponent, InteractUsingEvent>(OnInteractUsing);
  }

  private void OnBeingEquipped(Entity<RMCIdLockableNightVisionComponent> ent, ref BeingEquippedAttemptEvent args)
  {
    if (args.Cancelled)
        return;
        
    // require a valid ID to equip the sight
    if (!_idCard.TryFindIdCard(args.Equipee, out var idCard) || string.IsNullOrWhiteSpace(idCard.Comp.FullName))
    {
        args.Cancelled = true;
        // args.Reason = ""; requires a valid id
        // do smallcaution popupclient here
        return;
    }

    var name = idCard.Comp.FullName;

    // first valid equip locks the sight
    if (!ent.Comp.Locked)
    {
        ent.Comp.Locked = true;
        ent.Comp.OwnerName = name;
        Dirty(ent);

        // sight locked
        // do smallcaution popupclient here
        return;
    }

    // already owned by this ID
    if (ent.Comp.OwnerName == name)
        return;

    // otherwise, wrong ID
    args.Cancelled = true;

    // already locked to owner
    // do smallcaution popupclient here
  }
  
  private void OnInteractUsing(Entity<RMCIdLockableNightVisionComponent> ent, ref InteractUsingEvent args)
  {
    if (args.Handled || !ent.Comp.Locked)
        return;

    if (!_idCard.TryGetIdCard(args.Used, out var idCard))
        return;

    if (string.IsNullOrWhiteSpace(idCard.Comp.Fullname))
        return;

    var name = idCard.Comp.FullName;

    if (name == ent.Comp.OwnerName)
    {
        ent.Comp.Locked = false;
        ent.Comp.OwnerName = null;
        Dirty(ent);

        args.Handled = true;
        return;
    }

    args.Handled = true;
  }
}
