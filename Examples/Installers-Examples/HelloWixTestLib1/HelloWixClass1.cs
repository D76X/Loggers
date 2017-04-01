namespace HelloWixTestLib1 {
    public class HelloWixClass1 {
        public HelloWixClass1() {
            Message = nameof(HelloWixClass1);
        }

        public HelloWixClass1(string message) {
            Message = message;
        }

        public string Message { get; set; }
    }
}
