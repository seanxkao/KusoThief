#if AKTSK_DOTWEEN

using DG.Tweening;
using System;
using UnityEngine;
using System.Collections.Generic;

public static partial class ExtensionMethods
{
    public static Sequence DOJellyScale(
        this Transform transform,
        float time,
        float strength,
        Vector3 position,
        Action callback = null
    )
    {
        var originScale = transform.localScale;
        var sequence = DOTween.Sequence();
        var scale = Vector3.Scale(originScale, new Vector3(1.1f, 0.9f, 1f)) * strength;
        sequence.Append(transform.DOScale(scale, time).SetEase(Ease.InOutQuad));

        scale = Vector3.Scale(originScale, new Vector3(0.95f, 1.05f, 1f)) * strength;
        sequence.Insert(0, transform.DOLocalMove(position + new Vector3(0, -8, 0) * strength, time).SetEase(Ease.InOutQuad));
        sequence.Append(transform.DOScale(scale, time).SetEase(Ease.InOutQuad));
        sequence.Insert(time, transform.DOLocalMove(position, time).SetEase(Ease.InOutQuad));
        sequence.AppendCallback(delegate ()
        {
            if (callback != null)
            {
                callback();
            }
        });

        scale = Vector3.Scale(originScale, new Vector3(1.02f, 0.98f, 1f)) * strength;
        sequence.Append(transform.DOScale(scale, time).SetEase(Ease.InOutQuad));
        sequence.Insert(time * 2, transform.DOLocalMove(position + new Vector3(0, -2, 0) * strength, time).SetEase(Ease.InOutQuad));

        scale = Vector3.Scale(originScale, new Vector3(0.99f, 1.01f, 1f)) * strength;
        sequence.Append(transform.DOScale(scale, time).SetEase(Ease.InOutQuad));
        sequence.Insert(time * 3, transform.DOLocalMove(position, time).SetEase(Ease.InOutQuad));
        sequence.Append(transform.DOScale(originScale, time)).OnComplete(() =>
        {
            transform.localScale = originScale;
        });
        sequence.Insert(time * 4, transform.DOLocalMove(position, time));

        return sequence;
    }

    public static Tweener DORotate(this Transform transform, float time, float times, bool isClockwise = true)
    {
        var multiplier = isClockwise ? 1 : -1;
        var rotation = new Vector3(0, 0, times * 360 * multiplier);
        return transform.DOLocalRotate(rotation, time, RotateMode.LocalAxisAdd);
    }

    public static Sequence DOShake(this GameObject obj, float time, int times, float range, float delay = 0.0f)
    {
        var sequence = DOTween.Sequence();
        for (int i = 0; i < times; ++i)
        {
            var position = CreateRandom(obj, range);
            sequence.Append(obj.transform.DOLocalMove(position, time));
        }
        sequence.Append(obj.transform.DOLocalMove(obj.transform.localPosition, time));
        sequence.Play();

        return sequence;
    }

    public enum ArcOffsetType
    {
        Unadjusted,
        Normal,
        Inverted,
        Random
    }

    public static Sequence DOArc(this Transform obj, Vector3 target, float time, float delay, float offsetAmount, Vector3 symmetry, bool lookAt, ArcOffsetType type = ArcOffsetType.Normal, DG.Tweening.Ease arcEase = Ease.InOutSine)
    {
        var sequence = DOTween.Sequence();

        var source = obj.localPosition;
        var diff = (source - target).normalized;
        var rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        var aimRotation = Quaternion.Euler(0f, 0f, rot_z + 90);

        var mid = (source + target) / 2f;
        var arcOffsetDirection = Quaternion.Euler(0f, 0f, 90f) * (source - target).normalized;

        switch (type)
        {
            case ArcOffsetType.Unadjusted:
                break;
            case ArcOffsetType.Normal:
                if (Vector3.Dot(arcOffsetDirection, symmetry) < 0)
                {
                    arcOffsetDirection = Quaternion.Euler(0f, 0f, 180f) * arcOffsetDirection;
                }
                break;
            case ArcOffsetType.Inverted:
                if (Vector3.Dot(arcOffsetDirection, symmetry) >= 0)
                {
                    arcOffsetDirection = Quaternion.Euler(0f, 0f, 180f) * arcOffsetDirection;
                }
                break;
            case ArcOffsetType.Random:
                var random = UnityEngine.Random.Range(-1, 1);
                if (random > 0)
                {
                    arcOffsetDirection = Quaternion.Euler(0f, 0f, 180f) * arcOffsetDirection;
                }
                break;
        }

        var arcPoint = mid + arcOffsetDirection * offsetAmount;

        var arcPath = new List<Vector3>();
        arcPath.Add(arcPoint);
        arcPath.Add(target);


        if (lookAt)
        {
            sequence.Append(obj.transform.DORotate(aimRotation.eulerAngles, delay, RotateMode.Fast));
            sequence.Append(obj.DOLocalPath(arcPath.ToArray(), time, PathType.CatmullRom, PathMode.TopDown2D, 10, Color.red).SetLookAt(0.01f, Vector3.forward, Vector3.right).SetEase(arcEase));
        }
        else
        {
            sequence.AppendInterval(delay);
            sequence.Append(obj.DOLocalPath(arcPath.ToArray(), time, PathType.CatmullRom, PathMode.TopDown2D, 10, Color.red).SetEase(arcEase));
        }

        sequence.Play();

        return sequence;
    }

    public static Tweener DOJumpParabola(this Transform obj, Vector3 target, float time, float height)
    {
        var source = obj.localPosition;
        var midX = (source.x + target.x) / 2f;
        var midY = source.y + height;
        var mid = new Vector3(midX, midY, source.z);

        var arcPath = new List<Vector3>();
        arcPath.Add(source);
        arcPath.Add(mid);
        arcPath.Add(target);

        return obj.DOLocalPath(arcPath.ToArray(), time, PathType.CatmullRom);
    }

    public static Sequence DOJumpParabola(this Transform obj, Vector3 target, float distanceTime, float baseDistance, float height)
    {
        var source = obj.localPosition;
        var midX = (source.x + target.x) / 2f;
        var midY = (source.y > target.y ? source.y : target.y) + height;
        var mid = new Vector3(midX, midY, source.z);
        var distance = Vector3.Distance(source, mid) + Vector3.Distance(mid, target);
        var time = distance / baseDistance * distanceTime;

        var arcPath = new List<Vector3>();
        arcPath.Add(mid);
        arcPath.Add(target);

        var sequence = DOTween.Sequence();
        sequence.Append(obj.DOLocalPath(arcPath.ToArray(), time));
        sequence.SetEase(Ease.Linear);

        return sequence;
    }

    private static Vector3 CreateRandom(GameObject obj, float range)
    {
        var randomX = UnityEngine.Random.Range(-range, range);
        var randomY = UnityEngine.Random.Range(-range, range);
        var random = new Vector3(randomX, randomY, 0);

        return obj.transform.localPosition + random;
    }
}

#endif
