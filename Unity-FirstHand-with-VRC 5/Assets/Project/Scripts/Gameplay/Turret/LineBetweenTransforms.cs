/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * Licensed under the Oculus SDK License Agreement (the "License");
 * you may not use the Oculus SDK except in compliance with the License,
 * which is provided at the time of installation or download, or which
 * otherwise accompanies this software in either electronic or hard copy form.
 *
 * You may obtain a copy of the License at
 *
 * https://developer.oculus.com/licenses/oculussdk/
 *
 * Unless required by applicable law or agreed to in writing, the Oculus SDK
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections.Generic;
using UnityEngine;

namespace Oculus.Interaction.ComprehensiveSample
{
    /// <summary>
    /// Sets a LinerRenderers positions to match a set of ordered transforms
    /// Used to render the line from the players hand to the raycast target during the turret sequence
    /// </summary>
    [RequireComponent(typeof(LineRenderer)), ExecuteInEditMode]
    public class LineBetweenTransforms : MonoBehaviour
    {
        [SerializeField]
        private List<Transform> _transforms;

        [SerializeField]
        private bool _smooth;

        private LineRenderer _lineRenderer;
        private static Vector3[] _positions = new Vector3[20];

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            if (!_smooth)
            {
                if (_lineRenderer.positionCount != _transforms.Count)
                {
                    _lineRenderer.positionCount = _transforms.Count;
                }
                for (int i = 0; i < _transforms.Count; i++)
                {
                    _lineRenderer.SetPosition(i, _transforms[i].position);
                }
            }
            else
            {
                Transform start = _transforms[0];
                Transform end = _transforms[_transforms.Count - 1];
                GetBezierPositions(start, end, _positions);

                for (int i = 0; i < _positions.Length; i++)
                {
                    _lineRenderer.SetPosition(i, _positions[i]);
                }
            }
        }

        private static void GetBezierPositions(Transform start, Transform end, Vector3[] positions)
        {
            var line = end.position - start.position;
            var midPointLin = start.position + line * 0.5f;
            var plane = new Plane(line, midPointLin);
            plane.Raycast(new Ray(start.position, start.forward), out var midBezDist);
            var midBez = start.position + start.forward * midBezDist;
            Debug.DrawLine(midBez, midBez + Vector3.up * 0.1f);

            Vector3 p0 = start.position;
            Vector3 p1 = midBez;
            Vector3 p2 = end.position;
            for (int i = 0; i < positions.Length; i++)
            {
                var t = i / (positions.Length - 1.0f);
                positions[i] = (1.0f - t) * (1.0f - t) * p0 + 2.0f * (1.0f - t) * t * p1 + t * t * p2;
            }
        }
    }
}
