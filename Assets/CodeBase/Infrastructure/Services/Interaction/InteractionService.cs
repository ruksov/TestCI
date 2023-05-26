using Gobi.Infrastructure.Services.Input;
using UnityEngine;

namespace Gobi.Infrastructure.Services.Interaction
{
  public class InteractionService
  {
    private const string InteractableLayerName = "Interactable";

    private readonly IInputService m_inputService;
    private readonly int m_interactableMask;

    private Interactable m_interactable;

    public InteractionService(IInputService inputService)
    {
      m_inputService = inputService;
      m_inputService.Touch += Interact;
      m_inputService.TouchEnd += InteractEnd;

      m_interactableMask = LayerMask.GetMask(InteractableLayerName);
    }

    private void Interact()
    {
      m_interactable = RaycastInteractable();

      if (m_interactable)
        m_interactable.IsTouched = true;
    }

    private void InteractEnd()
    {
      if (m_interactable)
        m_interactable.IsTouched = false;
    }

    private Interactable RaycastInteractable()
    {
      RaycastHit2D hit = Physics2D.Raycast(
        WorldTouchPosition(),
        Camera.main.transform.forward,
        Mathf.Infinity,
        m_interactableMask);

      return hit.collider ? hit.collider.GetComponent<Interactable>() : null;
    }

    private Vector3 WorldTouchPosition() =>
      Camera.main.ScreenToWorldPoint(m_inputService.TouchPosition());
  }
}