using System;
using Godot;

public partial class UpgradeSelector : Container {
	public override void _Ready() {
		var upgradePanels = GetNode<Container>("./Upgrades").GetChildren();
		for (var i = 0; i < 3; i++) {
			var upgradePanel = upgradePanels[i];
			var upgrade = GetUpgrades()[i];

			upgradePanel.GetNode<Label>("./Heading").Text = upgrade.Title;
			upgradePanel.GetNode<RichTextLabel>("./Description").Text = upgrade.Description;
			upgradePanel.GetNode<Button>("./Button").ButtonDown += () => {
				upgrade.DoUpgrade();
				FinalizeUpgradeSelection();
			};
		}

		Events.I.PlayerSelectingLevelUp += () => {
			Show();
		};
	}

	public override void _Process(double delta) {
	}

	private void FinalizeUpgradeSelection() {
		Hide();
		Events.I.EmitSignal(Events.SignalName.PlayerSelectedLevelUp);
	}


	// TODO: This can be extracted out and can be considered as a "stub" for now
	// to emulate how the UpgradeSelector would fetch a set of upgrades to render
	// the UI and allow a user to select / upgrade their character behaviour.
	// Can be abstracted and made dynamic at a later stage, once more upgrades are fleshed out.
	private struct Upgrade {
		public string Title { get; set; }
		public string Description { get; set; }
		public Action DoUpgrade { get; set; }
	}

	private Upgrade[] GetUpgrades() {
		return new Upgrade[]{
			new Upgrade {
				Title = "Projectile",
				Description = "Add one more basic projectile",
				DoUpgrade = () => {
					Globals.GameState.NumProjectileCountUpgrades += 1;
				}
			},
			new Upgrade {
				Title = "Garlic",
				Description = "Increase the radius of garlic",
				DoUpgrade = () => {
					Globals.GameState.NumGarlicUpgrades += 1;
				}
			},
			new Upgrade {
				Title = "Orb",
				Description = "Add orb that will rotate around character",
				DoUpgrade = () => {
					Globals.GameState.NumOrbs += 1;
				}
			}
		};
	}
}
