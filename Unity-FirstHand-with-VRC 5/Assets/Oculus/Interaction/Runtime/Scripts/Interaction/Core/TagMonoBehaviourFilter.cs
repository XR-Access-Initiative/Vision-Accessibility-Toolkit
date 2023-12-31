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

namespace Oculus.Interaction
{
    /// <summary>
    /// An MonoBehaviour Filter that uses tags to filter MonoBehaviours
    /// </summary>
    public class TagMonoBehaviourFilter : MonoBehaviour, IMonoBehaviourFilter
    {
        /// If there is at least one required tag, an interactable must meet one of them
        [SerializeField, Optional]
        private string[] _requireTags;

        /// An interactable must not meet any of the avoid tags
        [SerializeField, Optional]
        private string[] _avoidTags;

        private HashSet<string> _requireTagSet;
        private HashSet<string> _avoidTagSet;

        protected virtual void Start()
        {
            _requireTagSet = new HashSet<string>();
            _avoidTagSet = new HashSet<string>();

            foreach (string requireTag in _requireTags)
            {
                _requireTagSet.Add(requireTag);
            }

            foreach (string avoidTag in _avoidTags)
            {
                _avoidTagSet.Add(avoidTag);
            }
        }

        public bool FilterMonoBehaviour(MonoBehaviour monoBehaviour)
        {
            GameObject gameObject = monoBehaviour.gameObject;
            if (!gameObject.TryGetComponent(out TagSet tagSet)
                && _requireTagSet.Count > 0)
            {
                return false;
            }

            foreach (string tag in _requireTagSet)
            {
                if (!tagSet.ContainsTag(tag))
                {
                    return false;
                }
            }

            if (tagSet == null)
            {
                return true;
            }

            foreach (string tag in _avoidTagSet)
            {
                if (tagSet.ContainsTag(tag))
                {
                    return false;
                }
            }

            return true;
        }

        #region Inject

        public void InjectOptionalRequireTags(string[] requireTags)
        {
            _requireTags = requireTags;
        }
        public void InjectOptionalAvoidTags(string[] avoidTags)
        {
            _avoidTags = avoidTags;
        }

        #endregion
    }
}
