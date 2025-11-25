namespace APICatalogo.DTOs.Auth
{
    public record LoginResponseDTO(
        UserResponseDTO User,
        TokenResponse Token
    );
}
