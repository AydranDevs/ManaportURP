using UnityEngine;
using UnityEngine.InputSystem;

namespace PartyNamespace {

    public enum PartyMemberState { CurrentLeader, PreviousLeader, OldestLeader }
    public enum PartyLeader { Laurie, Mirabelle, Winsley }

    public class Party : MonoBehaviour {
        
        public PartyLeader partyLeader;
        public GameObject previousLeader;
        public GameObject oldestLeader;

        public GameObject[] members;

        private PartyCam cam;

        public float maxDistance;

        private void Start() {
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PartyCam>();
            cam.target = members[0].transform;
            partyLeader = PartyLeader.Laurie;
            previousLeader = members[1];
            oldestLeader = members[2];
        }

        public void NextPartyMember(InputAction.CallbackContext context) {
            if (!context.started) return;
            int index = (int)partyLeader;

            index++;
            if (index > 2) {
                index = 0;
            }else if (index < 0) {
                index = 2;
            }

            previousLeader = members[(int)partyLeader];
            partyLeader = (PartyLeader)index;
            cam.target = members[index].transform;
            cam.PartyLeaderChanged();
            PartyLeaderChanged();
        }

        public void PreviousPartyMember(InputAction.CallbackContext context) {
            if (!context.started) return;
            int index = (int)partyLeader;

            index--;
            if (index > 2) {
                index = 0;
            }else if (index < 0) {
                index = 2;
            }

            previousLeader = members[(int)partyLeader];
            partyLeader = (PartyLeader)index;
            cam.target = members[index].transform;
            cam.PartyLeaderChanged();
            PartyLeaderChanged();
        }

        private void PartyLeaderChanged() {
            foreach (GameObject member in members) {
                PartyMember partyMember = member.GetComponent<PartyMember>();
                if (member == members[(int)partyLeader]) {
                    partyMember.partyMemberState = PartyMemberState.CurrentLeader; 
                } else if (member == previousLeader) {
                    partyMember.partyMemberState = PartyMemberState.PreviousLeader; 
                } else { 
                    partyMember.partyMemberState = PartyMemberState.OldestLeader;
                }
            }
        }

        private void Update() {
            foreach (GameObject member in members) {
                if (member != members[(int)partyLeader] && member != previousLeader) {
                    oldestLeader = member;
                }
            }
        }
    }
}
