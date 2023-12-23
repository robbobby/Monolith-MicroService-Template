using UserApi.Repository;

namespace UserApi.Service;

public class UserService(UserRepository userRepository) {
    public IReadOnlyList<T> GetAll<T>() where T : class {
        return userRepository.GetAll<T>().ToList();
    }

    public void Test() {
        var thing = userRepository.GetAll();
    }
}