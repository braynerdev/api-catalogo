# 🔧 REINICIE A API COM AS NOVAS CONFIGURAÇÕES

## Mudanças Feitas no Backend

1. ✅ **launchSettings.json** - Adicionado `http://0.0.0.0:5000` para aceitar conexões externas
2. ✅ **CorsConf.cs** - Configurado para permitir qualquer origem (necessário para React Native)

## Como Reiniciar

### Opção 1: Visual Studio

1. **Pare** a API (Shift+F5 ou botão Stop)
2. **Inicie** novamente (F5 ou botão Play)
3. Verifique no console se aparece:
   ```
   Now listening on: http://0.0.0.0:5000
   ```

### Opção 2: Terminal/CMD

```bash
cd c:\Users\jbray\DEV\api-catalogo\APICatalogo
dotnet run
```

Deve aparecer:
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:7157
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5050
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://0.0.0.0:5000
```

## ✅ Verificar se está Funcionando

Abra o navegador e acesse:
```
http://192.168.0.106:5000/swagger
```

Deve abrir a página do Swagger.

## 🚨 Problemas Comuns

### "Address already in use"
A porta 5000 já está em uso. Soluções:
- Mate o processo usando a porta 5000
- Ou mude para outra porta (ex: 5001) no `launchSettings.json` e no frontend

### Firewall Bloqueando
Execute como Administrador ou adicione exceção:
```powershell
netsh advfirewall firewall add rule name="ASP.NET Core" dir=in action=allow protocol=TCP localport=5000
```

### Ainda não funciona
Tente usar o IP específico ao invés de 0.0.0.0:
```json
"applicationUrl": "http://192.168.0.106:5000"
```

---

**Próximo passo:** Reinicie a API e teste novamente no React Native! 🚀
