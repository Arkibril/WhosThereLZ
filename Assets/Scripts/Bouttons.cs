using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bouttons : MonoBehaviour
{
    [Header("Background")]
    public GameObject backgroundTete, backgroundHaut, backgroundBas, backgroundChaussure;
    public GameObject backgroundCouleurDePeau, backgroundFormeDeTete, backgroundCheveux, backgroundYeux;
    public GameObject backgroundCouleurDeCheveux;

    [Header("Menu")]
    public GameObject menuPrincipal, menuSecondaire, menuTertiaire, quatriemeMenu;

    [Header("Reference")]
    public SkinPersonalisation _skinPersonalisation;
    public Animator anim;

    public void BouttonReset()
    {
        _skinPersonalisation.bodyIndex = 0;
        _skinPersonalisation.eyesIndex = 7;
        _skinPersonalisation.headIndex = 0;
        _skinPersonalisation.hairIndex = 6;
        _skinPersonalisation.hairColorIndex = 35;

        _skinPersonalisation.chaussuresIndex = 0;
        _skinPersonalisation.basIndex = 0;
        _skinPersonalisation.hautIndex = 0;
    }

    #region Menu

    private void MenuPrincipal()
    {
        menuPrincipal.SetActive(true);

        anim.SetBool("Face", false);
        anim.SetBool("Haut", false);
        anim.SetBool("Bas", false);
        anim.SetBool("Chaussure", false);
    }

    private void ResetMenuPrincipal()
    {
        menuPrincipal.SetActive(false);
    }

    private void MenuSecondaire()
    {
        menuSecondaire.SetActive(true);

        ResetMenuPrincipal();
    }

    private void MenuTertiaire()
    {
        menuTertiaire.SetActive(true);

        ResetMenuPrincipal();
    }

    private void QuatriemeMenu()
    {
        quatriemeMenu.SetActive(true);

        ResetMenuPrincipal();
    }
    #endregion

    #region Retour

    public void RetourTete()
    {
        menuTertiaire.SetActive(false);

        backgroundTete.SetActive(true);

        backgroundHaut.SetActive(false);
        backgroundBas.SetActive(false);
        backgroundChaussure.SetActive(false);
    }

    public void RetourTertiaire()
    {
        quatriemeMenu.SetActive(false);
    }

    public void RetourPrincipal()
    {
        menuSecondaire.SetActive(false);

        MenuPrincipal();
    }
    #endregion

    #region Boutton Menu Secondaire
    public void BouttonTete()
    {
        backgroundTete.SetActive(true);
        backgroundHaut.SetActive(false);
        backgroundBas.SetActive(false);
        backgroundChaussure.SetActive(false);


        MenuSecondaire();

        anim.SetBool("Face", true);
        anim.SetBool("Haut", false);
        anim.SetBool("Bas", false);
        anim.SetBool("Chaussure", false);

    }

    public void BouttonHaut()
    {
        backgroundTete.SetActive(false);
        backgroundHaut.SetActive(true);
        backgroundBas.SetActive(false);
        backgroundChaussure.SetActive(false);

        MenuSecondaire();
        menuTertiaire.SetActive(false);
        quatriemeMenu.SetActive(false);

        anim.SetBool("Face", false);
        anim.SetBool("Haut", true);
        anim.SetBool("Bas", false);
        anim.SetBool("Chaussure", false);


    }

    public void BouttonBas()
    {
        backgroundTete.SetActive(false);
        backgroundHaut.SetActive(false);
        backgroundBas.SetActive(true);
        backgroundChaussure.SetActive(false);

        MenuSecondaire();
        menuTertiaire.SetActive(false);
        quatriemeMenu.SetActive(false);

        anim.SetBool("Face", false);
        anim.SetBool("Haut", false);
        anim.SetBool("Bas", true);
        anim.SetBool("Chaussure", false);

    }
    public void BouttonChaussure()
    {
        backgroundTete.SetActive(false);
        backgroundHaut.SetActive(false);
        backgroundBas.SetActive(false);
        backgroundChaussure.SetActive(true);

        MenuSecondaire();
        menuTertiaire.SetActive(false);
        quatriemeMenu.SetActive(false);

        anim.SetBool("Face", false);
        anim.SetBool("Haut", false);
        anim.SetBool("Bas", false);
        anim.SetBool("Chaussure", true);


    }

    #endregion

    #region Boutton menu Tertiaire

    #region Couleur de peau
    public void CouleurDePeau()
    {
        backgroundTete.SetActive(false);

        backgroundCouleurDePeau.SetActive(true);
        backgroundFormeDeTete.SetActive(false);
        backgroundCheveux.SetActive(false);
        backgroundYeux.SetActive(false);

        MenuTertiaire();

    }

    public void CouleurDePeauButton(int Button)
    {
        _skinPersonalisation.bodyIndex = Button;
    }
    #endregion

    #region Forme de tete
    public void FormeDeTete()
    {
        backgroundTete.SetActive(false);

        backgroundCouleurDePeau.SetActive(false);
        backgroundFormeDeTete.SetActive(true);
        backgroundCheveux.SetActive(false);
        backgroundYeux.SetActive(false);

        MenuTertiaire();
    }

    public void FormeDeTeteButton(int Button)
    {
        _skinPersonalisation.headIndex = Button;
    }

    #endregion

    #region Cheveux
    public void Cheveux()
    {
        backgroundTete.SetActive(false);

        backgroundCouleurDePeau.SetActive(false);
        backgroundFormeDeTete.SetActive(false);
        backgroundCheveux.SetActive(true);
        backgroundYeux.SetActive(false);

        MenuTertiaire();
    }

    public void CheveuxButton(int Button)
    {
        _skinPersonalisation.hairIndex = Button;
    }
    #endregion

    #region Yeux
    public void Yeux()
    {
        backgroundTete.SetActive(false);

        backgroundCouleurDePeau.SetActive(false);
        backgroundFormeDeTete.SetActive(false);
        backgroundCheveux.SetActive(false);
        backgroundYeux.SetActive(true);

        MenuTertiaire();
    }

    public void YeuxButton(int Button)
    {
        _skinPersonalisation.eyesIndex = Button;
    }
    #endregion

    #region vetements
    #region Haut
    public void HautButton(int Button)
    {
        _skinPersonalisation.hautIndex = Button;
    }
    #endregion

    #region Bas
    public void BasButton(int bas)
    {
        _skinPersonalisation.basIndex = bas;
    }
    #endregion

    #region Chaussure
    public void ChaussureButton(int chaussure)
    {
        _skinPersonalisation.chaussuresIndex = chaussure;
    }
    #endregion
    #endregion

    #endregion

    #region Quatrieme menu

    #region Couleur de cheuveux
    public void CouleurDeCheveux()
    {
        backgroundCouleurDeCheveux.SetActive(true);

        QuatriemeMenu();
    }

    public void CouleurDeCheveuxbutton(int Button)
    {
        _skinPersonalisation.hairColorIndex = Button;
    }
    #endregion

    #endregion

}
