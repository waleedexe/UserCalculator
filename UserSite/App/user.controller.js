(function () {
    'user strict';
    angular.module('app')
        .controller('UserController', UserController);

    function UserController($http, $scope) {
        vm = this;
        var displayModes = {
            ListUsers: 'ListUsers',
            NewUser: 'NewUser',
            LoggedUser: 'LoggedUser',
            NewOperation: 'NewOperation',
            ListOperations: 'ListOperations'
        };
        var operationCodes = {
            ADD: { value: 1, name: "Addition", code: "+" },
            SUBTRACT: { value: 2, name: "Subtraction", code: "-" },
            DIVIDE: { value: 3, name: "Division", code: "/" }
        };
        var defaultUserOperation = {
            FirstNumber: '',
            SecondNumber: '',
            Operation: '',
            UserId: 0
        };
        var defaultUser = {
            LoginId: '',
            UserName: ''
        };
        vm.pageState = {
            mode: displayModes.ListUsers,
            isNewUser: false,
            isLoggedIn: false,
            isUserSearch: true,
            isNewOperation: false,
            isOperationList: false
        };
        vm.newUser = angular.copy(defaultUser);
        vm.users = [];
        vm.userLoginFilter = '';
        vm.loggedinUser = {
            LoginId: '',
            UserName: ''
        };
        vm.userOperations = [];
        vm.newUserOperation = angular.copy(defaultUserOperation);
        vm.opMessages = [];

        getUsers();

        vm.NewUser = function () {
            changeState(displayModes.NewUser);
        };
        vm.saveUser = function (userForm) {
            if (!userForm.$valid) {
                return;
            }

            $http.post('/api/User', vm.newUser).then(
                function (result) {
                    console.log(result);
                    vm.users.push(result.data);

                    changeState(displayModes.ListUsers);
                    resetUserForm(userForm);
                },
                function (error) {
                    console.log(error);
                });
        }
        vm.cancelNewUser = function (userForm) {
            changeState(displayModes.ListUsers);
            resetUserForm(userForm);
        }
        vm.isUserFieldValid = function (userField) {
            return userField.$pristine || !userField.$error.required;
        }
        vm.isUserFormValid = function (userForm) {
            return vm.isUserFieldValid(userForm.userId) && vm.isUserFieldValid(userForm.userName);
        }

        vm.LoginUser = function (loginId, userName) {
            vm.loggedinUser = vm.users.find(function (u) {
                return u.LoginId === loginId;
            });
            getUserOperations(vm.loggedinUser.Id);
            changeState(displayModes.LoggedUser);
        }
        vm.LogoutUser = function () {
            vm.loggedinUser = {};
            changeState(displayModes.ListUsers);
        }

        function resetUserForm(userForm) {
            userForm.$setPristine();
            vm.newUser = angular.copy(defaultUser);
        }

        function getUsers() {
            $http.get('/api/User').then(
                function (result) {
                    console.log(result);
                    vm.users = result.data;
                },
                function (error) {
                    console.log(error);
                });
        }

        // User operations
        function getUserOperations(id) {
            $http.get('/api/UserOperation/' + id).then(
                function (result) {
                    console.log(result);
                    vm.userOperations = result.data;
                },
                function (error) {
                    console.log(error);
                });
        }

        vm.NewUserOperation = function () {
            changeState(displayModes.NewOperation);
        }
        vm.cancelNewUserOperation = function (userOperationForm) {
            changeState(displayModes.ListOperations);
            resetUserOperationForm(userOperationForm);
        }
        vm.saveUserOperation = function (userOperationForm) {
            if (!userOperationForm.$valid) {
                return;
            }

            vm.opMessages = [];
            vm.newUserOperation.UserId = vm.loggedinUser.Id;
            $http.post('/api/UserOperation', vm.newUserOperation).then(
                function (result) {
                    console.log(result);
                    if (result.data.Messages.length > 0) {
                        vm.opMessages = result.data.Messages.map(function (m) {
                            return m.Details;
                        });
                        userOperationForm.firstNumber.$setValidity("message", false);
                        return;
                    }
                    vm.userOperations.push(result.data);

                    changeState(displayModes.ListOperations);
                    resetUserOperationForm(userOperationForm);
                },
                function (error) {
                    console.log(error);
                });
        }
        vm.resolveOperation = function (operation) {
            for (var opName in operationCodes) {
                var op = operationCodes[opName];
                if (op.value == operation) {
                    return op.code;
                }
            }

            return '';
        }
        vm.isOperationFieldValid = function (opField) {
            return opField.$pristine || !opField.$error.required;
        }
        vm.isUserOperationFormValid = function (userOperationForm) {
            if (!userOperationForm.$valid && vm.opMessages.length > 0) {
                return false;
            }

            return vm.isOperationFieldValid(userOperationForm.firstNumber)
                && vm.isOperationFieldValid(userOperationForm.secondNumber)
                && vm.isOperationFieldValid(userOperationForm.operationType);
        }

        function resetUserOperationForm(userOperationForm) {
            vm.opMessages = [];
            userOperationForm.firstNumber.$setValidity("message", true);
            userOperationForm.$setPristine();
            vm.newUserOperation = angular.copy(defaultUserOperation);
        }

        function changeState(newState) {
            vm.pageState.mode = newState;

            vm.pageState.isLoggedIn = (newState == displayModes.LoggedUser || newState == displayModes.NewOperation || newState == displayModes.ListOperations);
            vm.pageState.isNewUser = (newState == displayModes.NewUser);
            vm.pageState.isUserSearch = (newState == displayModes.ListUsers);
            vm.pageState.isNewOperation = (newState == displayModes.NewOperation);
            vm.pageState.isOperationList = (newState == displayModes.ListOperations || newState == displayModes.LoggedUser);
        }
    }
})();