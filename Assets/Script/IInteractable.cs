using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public string InteractionPrompt { get; }
    public void Hover();
    public void Unhover();
    public void Interact(Player player);
    public void VisualInteraction(Player player);
}
