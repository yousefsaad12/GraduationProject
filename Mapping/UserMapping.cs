
using Api.Dto;


namespace Api.Mapping
{
    public static class UserMapping
    {
        public static User ToUser(this UserRequest userRequest)
        {
            return new User
            {
                UserName = userRequest.userName,
                Email = userRequest.email,
            };
        }

        public static UserResponse ToUserResponse(this User user)
        {
            return new UserResponse
            {
                userName = user.UserName,
                email = user.Email,
            };
        }
    }
}