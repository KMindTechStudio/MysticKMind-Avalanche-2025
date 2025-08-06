using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginUI : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public Button loginButton;
    public TextMeshProUGUI messageText;
    public Button togglePasswordButton;
    public Image eyeIcon;
    public Sprite eyeOpenSprite;
    public Sprite eyeClosedSprite;

    private bool isPasswordHidden = true;

    void Start()
    {
        passwordInput.contentType = TMP_InputField.ContentType.Password;
        passwordInput.ForceLabelUpdate();
        loginButton.onClick.AddListener(OnLoginClick);
        togglePasswordButton.onClick.AddListener(TogglePasswordVisibility);
        messageText.text = "";
    }

    void TogglePasswordVisibility()
    {
        isPasswordHidden = !isPasswordHidden;
        if (isPasswordHidden)
        {
            passwordInput.contentType = TMP_InputField.ContentType.Password;
            eyeIcon.sprite = eyeClosedSprite;
        }
        else
        {
            passwordInput.contentType = TMP_InputField.ContentType.Standard;
            eyeIcon.sprite = eyeOpenSprite;
        }
        passwordInput.ForceLabelUpdate();
    }

    void OnLoginClick()
    {
        string username = usernameInput.text.Trim();
        string password = passwordInput.text.Trim();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            ShowMessage("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu", Color.yellow);
            return;
        }

        if (username.ToLower() == "admin" && password == "admin")
        {
            ShowMessage("Đăng nhập thành công!", Color.green);
            SceneManager.LoadScene("Map 1");
        }
        else
        {
            ShowMessage("Sai tên đăng nhập hoặc mật khẩu", Color.red);
        }
    }

    void ShowMessage(string text, Color color)
    {
        messageText.text = text;
        messageText.color = color;
    }

    public void PlayWithGuest()
    {
        SceneManager.LoadScene("Map 1");
    }
}
