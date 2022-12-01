using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepingLaserBeamWeapon : Weapon
{
    LineRenderer lineRenderer;

    private void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.widthMultiplier = 0.3f;
    }


    public override void Fire()
    {
        // What happens if the previous coroutine is still running though?
        StartCoroutine(SweepFireLaser());
    }

    IEnumerator SweepFireLaser()
    {
        // Needed to draw line.
        lineRenderer.positionCount = 2;

        requestDamageDealerEvent.Invoke(this);
        requestTargetEvent.Invoke(this);
        requestCooldownEvent.Invoke(this);

        // How long the coroutine runs.
        float durationRemaining = 0.25f + (currentCd * 0.5f);
        currentDamageDealerObject.GetComponent<LifeSpanLimiter>().spanRemaining = durationRemaining;

        currentTarget = Random.insideUnitCircle.normalized * 30;
        Vector3 endTarget = Random.insideUnitCircle.normalized * 30;
        // How much currentTarget changes by each iteration.
        Vector3 stepSize = (currentTarget - endTarget) / (durationRemaining / Time.fixedDeltaTime) * 2;


        while(durationRemaining > 0)
        {
            currentTarget += stepSize;
            currentTarget = currentTarget.normalized * 30f;

            RaycastHit2D firstHit;
            firstHit = Physics2D.Raycast(transform.position, currentTarget, 30f, LayerMask.GetMask("Enemy"));

            lineRenderer.SetPosition(0, transform.position);


            if (!firstHit.transform) 
            {
                lineRenderer.SetPosition(1, transform.position + currentTarget);
                currentDamageDealerObject.transform.position = transform.position;
            }
            else
            {
                lineRenderer.SetPosition(1, transform.position + (firstHit.distance * currentTarget / 30f));
                currentDamageDealerObject.transform.position = transform.position + (firstHit.distance * currentTarget / 30f);
            }



            durationRemaining -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        // Makes the line renderer not visible once firing stops.
        lineRenderer.positionCount = 0;
        yield return null;
    }
}
