﻿using Microsoft.EntityFrameworkCore;
using PISWF.domain.registermc.context.repository;
using PISWF.domain.registermc.model.entity;
using PISWF.domain.registermc.model.mapper;
using PISWF.domain.registermc.model.view;
using PISWF.infrasrtucture.auth.controller;
using PISWF.infrasrtucture.page;

namespace PISWF.domain.registermc.service;

// TODO: Не забыть только апдатать, сохранение на отдельную кнопку

public class RegistermcService
{
    private FileDocumentMapper FileDocumentMapper { get; }
    
    private FileDocumentRepository FileDocumentRepository { get; }
    
    private RegisterMcMapper RegisterMcMapper { get; }
    
    private RegisterMcRepository RegisterMcRepository { get; }

    public RegistermcService(
        FileDocumentMapper fileDocumentMapper,
        FileDocumentRepository fileDocumentRepository,
        RegisterMcMapper registerMcMapper,
        RegisterMcRepository registerMcRepository)
    {
        FileDocumentMapper = fileDocumentMapper;
        FileDocumentRepository = fileDocumentRepository;
        RegisterMcMapper = registerMcMapper;
        RegisterMcRepository = registerMcRepository;
    }

    public List<RegisterMCLong> Read()
    {
        return RegisterMcMapper.Map<List<RegisterMCLong>>(RegisterMcRepository.Entity);
    }
    
    public List<RegisterMCShort> Read(Page page)
    {
        return RegisterMcMapper.Map<List<RegisterMCShort>>(RegisterMcRepository.Entity
            .Include(x => x.Organization)
            .Include(x => x.Municipality)
            .Skip(page.Size*page.Number)
            .Take(page.Size)
        );
    }
    // TODO: Ограничение по ролям
    public List<RegisterMCShort> Read(Page page, Func<RegisterMC, bool> filter)
    { 
        return RegisterMcMapper.Map<List<RegisterMCShort>>(RegisterMcRepository.Entity
                .Where(filter)
                .Skip(page.Size*page.Number)
                .Take(page.Size)
            );
    }
    
    public RegisterMCLong Read(long id)
    {
        return RegisterMcMapper.Map<RegisterMCLong>(RegisterMcRepository.Entity
            .Include(x => x.Organization)
            .Include(x => x.Municipality)
            .Include(x => x.Documents)
            .FirstOrDefault(x => x.Equals(id))
        );
    }

    public RegisterMCShort Create(RegisterMCShort view)
    {
        var entity = RegisterMcMapper.Map<RegisterMC>(view);
        RegisterMcRepository.UpdateAndSave(entity);
        return view;
    }

    public RegisterMCLong Update(long id, RegisterMCLong view)
    {
        view.Id = id;
        var register = RegisterMcRepository.Entity.Find(id);
        register = RegisterMcMapper.Map(view, register);
        RegisterMcRepository.UpdateAndSave(register);
        return view;
    }

    public RegisterMCShort Delete(RegisterMCShort view)
    {
        var entity = RegisterMcMapper.Map<RegisterMC>(view);
        RegisterMcRepository.Entity.Remove(entity);
        return view;
    }
}