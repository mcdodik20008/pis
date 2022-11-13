﻿using PISWF.infrasrtucture.auth.controller;
using PISWF.infrasrtucture.auth.model.view;
using PISWF.infrasrtucture.filter;
using PISWF.domain.registermc.controller;



namespace PISWF.view;

public class Auth: Form
{
    private AuthController _authController;
    
    private IFilterFactory _factory;
    
    private FilterSorterMapper _filterSorterMapper;
    
    private RegistermcController _registermcController;
    
    public Auth(AuthController authController, IFilterFactory factory, FilterSorterMapper filterSorterMapper, RegistermcController registermcController)
    {
        _registermcController = registermcController;
        _factory = factory;
        _authController = authController;
        _filterSorterMapper = filterSorterMapper;
        InitializeItems();
        AddControls();

    }
    
    private void Authorization(object e, object sender)
    {
        if (loginBox.Text.Length > 0 && passwordBox.Text.Length > 0)
        {
            var userAuth = new UserAuth(loginBox.Text, passwordBox.Text);
            _authController.Authorization(userAuth);
        }

        if (!_authController.AutorizedUser.Login.Equals("guest"))
        {
            var dgVsForm = new DGVs(_authController, _factory, _filterSorterMapper, _registermcController);
            dgVsForm.ShowDialog();
        }
    }
    
    private void InitializeItems()
    {
        Size = new Size(480, 310);
        
        loginBox.Location = new Point(105, 56);
        loginBox.Size = new System.Drawing.Size(294, 27);
        
        passwordBox.Location = new Point(105, 87);
        passwordBox.Size = new System.Drawing.Size(294, 27);
        
        authorizationButton.Location = new Point(260, 120);
        authorizationButton.Size = new System.Drawing.Size(139, 34);
        authorizationButton.Text = "Вход";
        authorizationButton.Click += Authorization;
        
        loginLabel.Location = new System.Drawing.Point(36, 56);
        loginLabel.Size = new System.Drawing.Size(53, 20);
        loginLabel.Text = "Login";
        
        passwordLabel.Location = new System.Drawing.Point(14, 87);
        passwordLabel.Size = new System.Drawing.Size(75, 20);
        passwordLabel.Text = "Password";
    }
    
    private void AddControls()
    {
        Controls.Add(loginBox);
        Controls.Add(loginLabel);
        Controls.Add(passwordLabel);
        Controls.Add(passwordBox);
        Controls.Add(authorizationButton);
    }
    
    #region компоненты для формы
    
    private TextBox loginBox = new();
    private TextBox passwordBox = new();
    private Button authorizationButton = new();
    private Label loginLabel = new ();
    private Label passwordLabel = new ();

    #endregion
}