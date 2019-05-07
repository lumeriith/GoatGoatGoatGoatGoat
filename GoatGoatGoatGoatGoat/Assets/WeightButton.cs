using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
using TMPro;
public class WeightButton : MonoBehaviour
{

	private WeightScale weightScale;
	public float weightRequired = 10f;
	public Renderer targetRenderer;

	public UnityEvent onActivate;
	public UnityEvent onDeactivate;
    public TextMeshPro outputTextMeshPro;
    private AudioSource audioSource;
    public bool isActivated;
	

	private void Start ()
	{
		weightScale = GetComponentInChildren<WeightScale>();
        audioSource = GetComponent<AudioSource>();
    }

	private void FixedUpdate()
	{
		if (weightScale.weight >= weightRequired && !isActivated)
		{
			targetRenderer.material.color = Color.green;
			isActivated = true;
			onActivate.Invoke();

            if (audioSource != null) GetComponent<AudioSource>().Play();

        }
		else if (weightScale.weight < weightRequired && isActivated)
		{
			targetRenderer.material.color = Color.red;
			isActivated = false;
			onDeactivate.Invoke();
			
		}
        if (outputTextMeshPro != null)
        {
            outputTextMeshPro.text = Units.readableText(weightScale.weight) + "/" + Units.readableText(weightRequired);
        }
	}
}
