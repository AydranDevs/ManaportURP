using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion;
using Manapotion.PartySystem;
using Manapotion.StatusEffects;

public class StatusEffectsUIHandler : MonoBehaviour {
    [HideInInspector] public static StatusEffectsUIHandler Instance;

    [SerializeField] private GameObject statusPrefab;
    [SerializeField] private List<GameObject> statusViews;
    
    [SerializeField] private List<MemberStatusEffectsUI> members;
    [SerializeField] private MemberStatusEffectsUI currentMember;

    [System.Serializable]
    private class MemberStatusEffectsUI {
        public PartyMember member;
        public List<Buff> statuses;

        public MemberStatusEffectsUI(PartyMember member) {
            this.member = member;

            statuses = new List<Buff>();
            // foreach (var st in member.GetComponent<PartyMember>().buffs) {
            //     statuses.Add(st);
            // }
        }

        public void AddEffect(Buff effect) {
            statuses.Add(effect);
        }

        public void RemoveEffect(Buff effect) {
            statuses.Remove(effect);
        }
    }

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        Party.OnPartyLeaderChanged += RefreshView;
        ManaBehaviour.OnUpdate += Update;
        statusViews = new List<GameObject>();
        members = new List<MemberStatusEffectsUI>();
        
        foreach (var m in Party.Instance.members) {
            members.Add(new MemberStatusEffectsUI(m));
        }
    }

    private void RefreshView() {
        foreach (var mem in Party.Instance.members) {
            var pm = mem.GetComponent<PartyMember>();
            if (pm.partyMemberState == PartyMemberState.CurrentLeader) {
                foreach (var mseu in members) {
                    if (mseu.member == pm.gameObject) {
                        RemoveAllStatuses();
                        currentMember = mseu;
                        ShowStatuses(mseu);
                        return;
                    }
                }
            }
        }
    }

    private void RemoveAllStatuses() {
        if (statusViews.Count > 0) {
            foreach (var s in statusViews) {
                Destroy(s);
            }
            statusViews = new List<GameObject>();
        }
    }

    private void ShowStatuses(MemberStatusEffectsUI mem) {
        int i = 0;
        foreach (var m in mem.statuses) {
            statusViews.Add(Instantiate(statusPrefab, this.transform));
            // statusViews[i].GetComponent<StatusEffectPreviewUIHandler>().buffType = m.buffType;
            i++;
        }
    }

    private void Update() {
        UpdateStatusTimers(currentMember);
    }

    private void UpdateStatusTimers(MemberStatusEffectsUI mem) {
        for (int i = 0; i < statusViews.Count; i++) {
            double fixedTime = Math.Truncate((double)mem.statuses[i].time);
            string time = string.Format("{0}s", fixedTime);

            statusViews[i].GetComponent<StatusEffectPreviewUIHandler>().timer.text = time;
        }
    }

    public void AddStatus(GameObject m, Buff effect) {
        foreach (var mem in members) {
            if (mem.member == m) {
                mem.AddEffect(effect);
                return;
            }
        }
    }

    public void RemoveStatus(GameObject m, Buff effect) {
        foreach (var mem in members) {
            if (mem.member == m) {
                mem.RemoveEffect(effect);
                RefreshView();
                return;
            }
        }
    }
}
