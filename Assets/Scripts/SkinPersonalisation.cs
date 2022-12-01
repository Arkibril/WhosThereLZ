using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinPersonalisation : MonoBehaviour
{ 

    [Header("Materials")]
    public Material[] bodyMaterials;
    public Material[] headMaterials;
    public Material[] eyesMaterials;
    public Material[] chaussuresMaterial;
    public Material[] hautMaterial;
    public Material[] basMaterial;
    public Material[] hairMaterial;

    [Space(5)]
    [Header("Meshs")]
    public Mesh[] chaussuresMesh;
    public Mesh[] hautMesh;
    public Mesh[] basMesh;
    public Mesh[] headMesh;
    public Mesh[] corpsMesh;
    public Mesh[] hairMesh;

    [Space]
    [Header("Color")]
    public Color[] hairColor;


    [Header("Visage Index")]
    [Range(0, 10)]
    public int bodyIndex;
    [Space(5)]
    [Range(0, 10)]
    public int faceIndex;
    [Space(5)]
    [Range(0, 8)]
    public int eyesIndex;
    [Space(5)]
    [Range(0, 10)]
    public int headIndex;
    [Space(5)]
    [Range(0, 6)]
    public int hairIndex;
    [Range(0, 65)]
    public int hairColorIndex;

    [Space(5)]
    [Header("Vetements Index")]
    [Range(0, 4)]
    public int chaussuresIndex;
    [Range(0, 3)]
    public int hautIndex;
    [Range(0, 4)]
    public int basIndex;
    [Range(0, 2)]
    public int corpsIndex;


    [Header("GameObjects")]
    public GameObject body;
    public GameObject head;
    public GameObject eyesLeft;
    public GameObject eyesRight;
    public GameObject haut;
    public GameObject bas;
    public GameObject chaussures;
    public GameObject hair;

    [Header("Skinned Mesh Renderer")]
    private SkinnedMeshRenderer bodyRenderer;
    private SkinnedMeshRenderer headRenderer;
    private SkinnedMeshRenderer eyesLeftRenderer;
    private SkinnedMeshRenderer eyesRightRenderer;
    private SkinnedMeshRenderer hautRendrer;
    private SkinnedMeshRenderer basRendrer;
    private SkinnedMeshRenderer chaussuresRendrer;
    private SkinnedMeshRenderer hairRenderer;





    // Start is called before the first frame update
    void Start()
    {

        bodyIndex = 0;
        eyesIndex = 7;
        headIndex = 0;
        hairIndex = 6;
        hairColorIndex = 35;

        chaussuresIndex = 0;
        basIndex = 0;
        hautIndex = 0;

        bodyRenderer = body.GetComponent<SkinnedMeshRenderer>();
        eyesLeftRenderer = eyesLeft.GetComponent<SkinnedMeshRenderer>();
        eyesRightRenderer = eyesRight.GetComponent<SkinnedMeshRenderer>();
        hairRenderer = hair.GetComponent<SkinnedMeshRenderer>();

        hautRendrer = haut.GetComponent<SkinnedMeshRenderer>();
        basRendrer = bas.GetComponent<SkinnedMeshRenderer>();
        chaussuresRendrer = chaussures.GetComponent<SkinnedMeshRenderer>();

        headRenderer = head.GetComponent<SkinnedMeshRenderer>();

        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {

        EyesColorChoise();
        SkinColorChoise();
        HeadChoise();
        VetementsChoise();
        HairChoise();

    }

    void SkinColorChoise()
    {

        bodyRenderer.material = bodyMaterials[bodyIndex];
        faceIndex = bodyIndex + 0;

        headRenderer.material = headMaterials[faceIndex];
 

    }

    void EyesColorChoise()
    {
        eyesLeftRenderer.material = eyesMaterials[eyesIndex];
        eyesRightRenderer.material = eyesMaterials[eyesIndex];
    }

    void HeadChoise()
    {
        headRenderer.sharedMesh = headMesh[headIndex];
    }

    void VetementsChoise()
    {
        chaussuresRendrer.sharedMesh = chaussuresMesh[chaussuresIndex];
        chaussuresRendrer.material = chaussuresMaterial[chaussuresIndex];

        hautRendrer.sharedMesh = hautMesh[hautIndex];
        hautRendrer.material = hautMaterial[hautIndex];

        basRendrer.sharedMesh = basMesh[basIndex];
        basRendrer.material = basMaterial[basIndex];

        bodyRenderer.sharedMesh = corpsMesh[corpsIndex];

        if (hautIndex == 1 && basIndex == 2|| hautIndex == 1 && basIndex == 3)
        {
            corpsIndex = 2;
        }
        else if(hautIndex != 1 && basIndex == 2 || hautIndex != 1 && basIndex == 3)
        {
            corpsIndex = 1;
        }
        else
        {
            corpsIndex = 0;
        }
    }

    void HairChoise()
    {
        hairRenderer.sharedMesh = hairMesh[hairIndex];
        hairRenderer.material = hairMaterial[hairIndex];
        hairRenderer.material.color = hairColor[hairColorIndex];
    }



}
