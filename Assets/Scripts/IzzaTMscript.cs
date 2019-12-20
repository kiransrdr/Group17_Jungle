
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
public class IzzaTMscript : MonoBehaviour {
    public Button button;
    public Text input_field, txt;
    public bool isPalindrome;
    public string[] cubearr;
    private GameObject cube;
    private const int ScaleValue = 10;
    public Material cubemat;
    public Camera camera;
    public GameObject myPrefab;
    GameObject[] cubes, childs;
    Ray ray;
    RaycastHit hit;
    Vector3 position;
    char[] charArr;
    public int currentindex = 1;
    public GameObject optxt;
    public string state = "q0", status = "";
    public char move = 'R';
    void Start()
    {
        button.onClick.AddListener(onbtnClick);
        //GameObject.Find("Audiofile").GetComponent<AudioSource>().Play();
        ray = new Ray(transform.position, transform.forward);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && status == "")
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
        else if (status == "accepted")
        {
            optxt.GetComponent<TextMesh>().text = "✔";
            //  txt.GetComponent<Text>().fontSize = 30;
            txt.GetComponent<Text>().text = "accepted";

        }
        else if (status == "rejected")
        {
            optxt.GetComponent<TextMesh>().text = "✘";
            txt.GetComponent<Text>().text = "rejected";
            destroyObject(charArr);
        }
    }
    private void runTuringMachine()
    {

        
        for (int i = 0; i < (charArr.Length); i++)
        {
            Destroy(cubes[i]);
        }
        print("runTuringMachine");
        if (state == "q0")
        {
            if (charArr[currentindex] == '1') { charArr[currentindex] = 'b'; move = 'R'; state = "q1"; }
            else if (charArr[currentindex] == '0') { charArr[currentindex] = 'a'; state = "q1"; move = 'R'; }
            else { status = "rejected"; }
        }
        else if (state == "q1")
        {
            if (charArr[currentindex] == '0') { charArr[currentindex] = '0'; move = 'R'; }
            else if (charArr[currentindex] == '1') { charArr[currentindex] = '1'; move = 'R'; }
            else if (charArr[currentindex] == 'Δ') { charArr[currentindex] = 'Δ'; state = "q2"; move = 'L'; }
            else if (charArr[currentindex] == 'd') { charArr[currentindex] = 'd'; state = "q2"; move = 'L'; }
            else if (charArr[currentindex] == 'c') { charArr[currentindex] = 'c'; state = "q2"; move = 'L'; }
            else { status = "rejected"; }
        }
        else if (state == "q2")
        {
            if (charArr[currentindex] == '1') { charArr[currentindex] = 'd'; move = 'L'; state = "q3"; }
            else if (charArr[currentindex] == '0') { charArr[currentindex] = 'c'; move = 'L'; state = "q3"; }

            else { status = "rejected"; }
        }
        else if (state == "q3")
        {
            if (charArr[currentindex] == '0') { charArr[currentindex] = '0'; move = 'L'; state = "q4"; }
            else if (charArr[currentindex] == '1') { charArr[currentindex] = '1'; move = 'L'; state = "q4"; }
            else if (charArr[currentindex] == 'a' || charArr[currentindex] == 'b') { move = 'L'; state = "q5"; }

            else { status = "rejected"; }
        }
        else if (state == "q4")
        {
            if (charArr[currentindex] == '0' || charArr[currentindex] == '1') { move = 'L'; state = "q4"; }
            else if (charArr[currentindex] == 'a') { charArr[currentindex] = 'a'; move = 'R'; state = "q0"; }
            else if (charArr[currentindex] == 'b') { charArr[currentindex] = 'b'; move = 'R'; state = "q0"; }
            else { status = "rejected"; }
        }
        else if (state == "q5")
        {
            if (charArr[currentindex] == 'x' || charArr[currentindex] == 'a' || charArr[currentindex] == 'b') { move = 'L'; state = "q5"; }
            else if (charArr[currentindex] == 'Δ') { charArr[currentindex] = 'Δ'; move = 'R'; state = "q6"; }
            else { status = "rejected"; }
        }
        else if (state == "q6")
        {
            if (charArr[currentindex] == 'a') { charArr[currentindex] = 'Δ'; move = 'R'; state = "q7"; }
            else if (charArr[currentindex] == 'b') { charArr[currentindex] = 'Δ'; move = 'R'; state = "q8"; }
            else { status = "rejected"; }
        }
        else if (state == "q7")
        {
            if (charArr[currentindex] == 'x' || charArr[currentindex] == 'a' || charArr[currentindex] == 'b') { move = 'R'; state = "q7"; }
            else if (charArr[currentindex] == 'c') { charArr[currentindex] = 'x'; move = 'L'; state = "q5"; }
            else if (charArr[currentindex] == 'd') { charArr[currentindex] = 'x'; move = 'R'; state = "q9"; }
            else { status = "rejected"; }
        }
        else if (state == "q8")
        {
            if (charArr[currentindex] == 'x' || charArr[currentindex] == 'a' || charArr[currentindex] == 'b') { move = 'R'; }
            else if (charArr[currentindex] == 'd') { charArr[currentindex] = 'x'; move = 'L'; state = "q5"; }
            else if (charArr[currentindex] == 'c') { charArr[currentindex] = 'x'; move = 'R'; state = "q9"; }
            else { status = "rejected"; }
        }
        else if (state == "q9")
        {
            status = "accepted";
        }

        showCubes(charArr);
        print(state);
    }

    public void onSpacebarClick()
    {

    }

    public void onbtnClick()
    {
        string str = "ΔΔ";
        str += input_field.text.ToString();
        str += "ΔΔ";
        charArr = str.ToCharArray();
        string inputpattern = @"([0-1]+)$";
        Match match = Regex.Match(input_field.text, inputpattern);
        if (match.Success)
            showCubes(charArr);
        else
        {
            print("Invalid input");
            optxt.GetComponent<TextMesh>().text = "?";
            // showCubes(charArr);
        }
        GameObject.Find("Button").transform.position = new Vector3(300, 800f, -901f);
        GameObject.Find("InputField2").transform.position = new Vector3(300, 800f, -901f);
    }
    public void showCubes(char[] charArr)
    {
        int len = charArr.Length;
        cubes = new GameObject[len];
        childs = new GameObject[len];
        TextMesh tm = GetComponent<TextMesh>();
        //camera.transform.position= new Vector3(2.19f, -3, -3.78f);
        for (int i = 0; i < (len); i++)
        {
            generateCubes(cubes, i, charArr[i].ToString());
        }
        //generateCubes(cubes, charArr, len+1, "#");
    }
    public void generateCubes(GameObject[] cubes, int i, string txt){
        cubes[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cubes[i].transform.position = new Vector3(((i * 6) - 6f) + 300, 8f, -11f);
        cubes[i].transform.localScale = new Vector3(3f, 3f, 1f);
        cubes[i].GetComponent<MeshRenderer>().material = cubemat;

        GameObject child = new GameObject("childobject");
        child.transform.SetParent(cubes[i].transform);
        child.transform.position = new Vector3(((i * 6) - 6f) + 300, 8f, -11f);
        child.AddComponent<TextMesh>();
        child.GetComponent<TextMesh>().text = txt;
        child.GetComponent<TextMesh>().characterSize = 4;
        child.GetComponent<TextMesh>().fontSize = 4 * ScaleValue;
        child.GetComponent<TextMesh>().color = Color.white;
        child.GetComponent<TextMesh>().offsetZ = -8;
        child.GetComponent<TextMesh>().anchor = TextAnchor.MiddleCenter;
        child.GetComponent<TextMesh>().transform.localScale = child.GetComponent<TextMesh>().transform.localScale / ScaleValue;
    }
    public void destroyObject(char[] charArr)
    {
        for (int i = 0; i < (charArr.Length); i++)
        {
            Destroy(cubes[i]);

            Instantiate(myPrefab, new Vector3(((i * 6) - 6f) + 300, 8f, -11f), Quaternion.identity);
            // myPrefab.transform.localScale= new Vector3(3f, 3f, 1f);
            Destroy(gameObject);
        }
    }
}


