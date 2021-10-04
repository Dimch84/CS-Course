using System.Collections.Generic;

namespace ComposedMission
{
    public delegate double CalcDelegate(double val);

    public class FunctionsContainer {

        private Dictionary<string, CalcDelegate> dictionary = new Dictionary<string, CalcDelegate>();

        public CalcDelegate this[string i] {
            get {
                if (dictionary.ContainsKey(i)) {
                    return dictionary[i];
                }
                else {
                    return val => val;
                }
            }
            set {
                dictionary[i] = value;
            }
        }

        public List<string> getAllMissions() {
            return new List<string>(this.dictionary.Keys);
        }
    }
}
