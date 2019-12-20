using System.Text.RegularExpressions;
using UnityEngine; 
using UnityEngine.UI;
public class palindromescript : MonoBehaviour {
    public Button button;
    public Text input_field, txt;
    public bool isPalindrome;
    public string[] cubearr;
    private GameObject cube;
    public GameObject myPrefab;
    private const int ScaleValue = 10;
    public Material cubemat;
    public Camera camera;
    GameObject[] cubes, childs;
    public GameObject optxt;
    Ray ray;
    RaycastHit hit;
    Vector3 position;
    char[] charArr;
    public int currentindex = 1;
    public string state = "q0", status = "";
    
    public char move = 'R';
    void Start () { 
        button.onClick.AddListener(onbtnClick);
        GameObject.Find("Audiofile").GetComponent<AudioSource>().Play();
        ray = new Ray(transform.position, transform.forward);
    }
    void Update (){
        if (Input.GetKeyDown(KeyCode.Space)  )
        {
            if ( status != "accepted")
            {
                txt.GetComponent<Text>().text = " ";
                optxt.GetComponent<TextMesh>().text = "";
                Debug.Log("current" + currentindex);
                txt.GetComponent<Text>().text = state;
                optxt.GetComponent<TextMesh>().text = state;
                Physics.Raycast(ray, out hit);
                position = camera.transform.position;
                if (move == 'R') { position.x = position.x + 6f; currentindex++; }
                if (move == 'L') { position.x = position.x - 6f; currentindex--; }
                camera.transform.position = position;
                runTuringMachine();
            }
            else if (status != "rejected")
            {
                txt.GetComponent<Text>().text = " ";
                Debug.Log("current" + currentindex);
                txt.GetComponent<Text>().text = state;
                Physics.Raycast(ray, out hit);
                position = camera.transform.position;
                if (move == 'R') { position.x = position.x + 6f; currentindex++; }
                if (move == 'L') { position.x = position.x - 6f; currentindex--; }
                camera.transform.position = position;
                runTuringMachine();
            }
        }
        else if (status == "accepted") {

            optxt.GetComponent<TextMesh>().text = "✔";
            txt.GetComponent<Text>().text = "accepted";
        }
        else if (status == "rejected")
        {
            txt.GetComponent<Text>().text = "rejected";
            optxt.GetComponent<TextMesh>().text = "✘";
            destroyObject(charArr);
            print("rejected");
        }
    }
    private  void runTuringMachine(){
        for (int i = 0; i < (charArr.Length); i++)
        { 
            Destroy(cubes[i]);
        }
        print("runTuringMachine");
        if (state == "q0") {
            if (charArr[currentindex] == '1' || charArr[currentindex] == '2' || charArr[currentindex] == '3' || charArr[currentindex] == '4') { move = 'R'; }
            else if (charArr[currentindex] == 'Δ') { charArr[currentindex] = 'E'; state = "q1"; move = 'L'; }
            else { status = "rejected"; }
        }
        else if (state == "q1") {
            if (charArr[currentindex] == '1' || charArr[currentindex] == '2' || charArr[currentindex] == '3' || charArr[currentindex] == '4') { move = 'L'; }
            else if (charArr[currentindex] == 'Δ') { charArr[currentindex] = 'Δ'; state = "q2"; move = 'R'; }
            else { status = "rejected"; }
        }
        else if(state == "q2") {
            if (charArr[currentindex] == '1') { charArr[currentindex] = 'X'; move = 'R'; state = "q3"; }
            else if (charArr[currentindex] == '2') { charArr[currentindex] = 'X'; move = 'R'; state = "q4"; }
            else if(charArr[currentindex] == '3') { charArr[currentindex] = 'X'; move = 'R'; state = "q5"; }
            else if(charArr[currentindex] == '4') { charArr[currentindex] = 'X'; move = 'R'; state = "q6"; }
            else if(charArr[currentindex] == 'X') { charArr[currentindex] = 'X'; move = 'R'; state = "q2"; }
            else if(charArr[currentindex] == 'E') { charArr[currentindex] = 'E';  state = "q11"; }
            else { status = "rejected"; }
        }
        else if(state == "q3") {
            if (charArr[currentindex] == '1'  || charArr[currentindex] == '3' || charArr[currentindex] == '4' || charArr[currentindex] == 'X') { move = 'R'; state = "q3"; }
            else if(charArr[currentindex] == '2') { charArr[currentindex] = 'X'; move = 'L'; state = "q7"; }
            else { status = "rejected"; }
        }
        else if (state == "q4") {
            if (charArr[currentindex] == '2' || charArr[currentindex] == '3' || charArr[currentindex] == '4' || charArr[currentindex] == 'X') { move = 'R'; state = "q4"; }
            else if (charArr[currentindex] == '1') { charArr[currentindex] = 'X'; move = 'L'; state = "q8"; }
            else { status = "rejected"; }
        }
        else if(state == "q5") {
            if (charArr[currentindex] == '1' || charArr[currentindex] == '2' || charArr[currentindex] == '3' || charArr[currentindex] == 'X') { move = 'R'; state = "q5"; }
            else if(charArr[currentindex] == '4') { charArr[currentindex] = 'X'; move = 'L'; state = "q9"; }
            else { status = "rejected"; }
        }
        else if(state == "q6") {
            if (charArr[currentindex] == '1' || charArr[currentindex] == '2' || charArr[currentindex] == '4' || charArr[currentindex] == 'X') { move = 'R'; }
            else if(charArr[currentindex] == '3') { charArr[currentindex] = 'X'; move = 'L'; state = "q10"; }
            else { status = "rejected"; }
        }
        else if (state == "q7") {
            if (charArr[currentindex] == '1' || charArr[currentindex] == '2' || charArr[currentindex] == '3' || charArr[currentindex] == '4' || charArr[currentindex] == 'X') { move = 'L'; }
            else if(charArr[currentindex] == 'Δ') { charArr[currentindex] = 'Δ'; move = 'R'; state = "q2"; }
            else { status = "rejected"; }
        }
        else if(state == "q8")
        {
            if (charArr[currentindex] == '1' || charArr[currentindex] == '2' || charArr[currentindex] == '3' || charArr[currentindex] == '4' || charArr[currentindex] == 'X') { move = 'L'; }
            else if(charArr[currentindex] == 'Δ') { charArr[currentindex] = 'Δ'; move = 'R'; state = "q2"; }
            else { status = "rejected"; }
        }
        else if(state == "q9")
        {
            if (charArr[currentindex] == '1' || charArr[currentindex] == '2' || charArr[currentindex] == '3' || charArr[currentindex] == '4' || charArr[currentindex] == 'X') { move = 'L'; }
            else if(charArr[currentindex] == 'Δ') { charArr[currentindex] = 'Δ'; move = 'R'; state = "q2"; }
            else { status = "rejected"; }
        }
        else if(state == "q10")
        {
            if (charArr[currentindex] == '1' || charArr[currentindex] == '2' || charArr[currentindex] == '3' || charArr[currentindex] == '4' || charArr[currentindex] == 'X') { move = 'L'; }
            else if(charArr[currentindex] == 'Δ') { charArr[currentindex] = 'Δ'; move = 'R'; state = "q2"; }
            else { status = "rejected"; }
        }
        else if(state == "q11") {
            status = "accepted";
        }
        showCubes(charArr);
        print(state);
    }

    public void onSpacebarClick() {

    } 
     
    public void onbtnClick()
    {
        string str = "ΔΔ";
         str += input_field.text.ToString();
        str += "ΔΔ";
        charArr = str.ToCharArray();
        string inputpattern = @"([1-4]+)$";
        Match match = Regex.Match(input_field.text, inputpattern);
        if (match.Success)
        {
            showCubes(charArr);
            optxt.GetComponent<TextMesh>().text = "?";
        }
        else
        {
            print("Invalid input");
            showCubes(charArr);
        }
        GameObject.Find("Button").transform.position = new Vector3(300, 800f, -901f);
        GameObject.Find("InputField3").transform.position = new Vector3(300, 800f, -901f);
    }
    public void showCubes(char[] charArr) {
        int len = charArr.Length;
        cubes  = new GameObject[len];
        childs = new GameObject[len];
        TextMesh tm = GetComponent<TextMesh>();
        //camera.transform.position= new Vector3(2.19f, -3, -3.78f);
        for (int i = 0; i <(len); i++){
            generateCubes(cubes, i, charArr[i].ToString());
        }
        //generateCubes(cubes, charArr, len+1, "#");
    }
    public void generateCubes(GameObject[] cubes, int i,string txt) {
        cubes[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cubes[i].transform.position = new Vector3(((i * 6) - 5f) + 149.8f, 10.3f, -362.5f);
        cubes[i].transform.localScale = new Vector3(3f, 3f, 1f);
        cubes[i].GetComponent<MeshRenderer>().material = cubemat;

        GameObject child = new GameObject("childobject");
        child.transform.SetParent(cubes[i].transform);
        child.transform.position = new Vector3(((i * 6) - 5f) + 149.8f, 10.3f, -362.5f);
        child.AddComponent<TextMesh>();
        child.GetComponent<TextMesh>().text = txt; 
        child.GetComponent<TextMesh>().characterSize = 4;
        child.GetComponent<TextMesh>().fontSize = 4 * ScaleValue;
        child.GetComponent<TextMesh>().color = Color.white;
        child.GetComponent<TextMesh>().offsetZ = -8;
        child.GetComponent<TextMesh>().anchor = TextAnchor.MiddleCenter;
        child.GetComponent<TextMesh>().transform.localScale = child.GetComponent<TextMesh>().transform.localScale / ScaleValue;
    }
    public void destroyObject(char[] charArr) {
        for (int i = 0; i < (charArr.Length); i++)
        {
            Destroy(cubes[i]);

            Instantiate(myPrefab, new Vector3(((i * 6) - 5f) + 149.8f, 10.3f, -362.5f), Quaternion.identity);
// myPrefab.transform.localScale= new Vector3(3f, 3f, 1f);
            Destroy(gameObject);
        }
    }
}
