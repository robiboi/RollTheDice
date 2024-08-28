namespace RollTheDice.Model
{
    public class Die
    {
        public int FaceValue { get; set; }
        public int FaceFactor { get; set; }
        public Die() { }
        public Die(int faceValue, int faceFactor) { 
            FaceFactor = faceFactor;
            FaceValue = faceValue;
        }
    }
}
