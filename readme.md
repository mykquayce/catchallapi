Generate a random password:
```bash
openssl rand -base64 99 | sed 's/[^0-9A-Za-z]//g' | sed -z 's/\n//g'
```
Create a Docker secret for the password:
```bash
echo -n .. | docker secret create kestrel_certificates_default_password -
```
Export the self-signed certificate with the password:
```bash
dotnet dev-certs https --export-path ~/.aspnet/https/cert.pfx --password ..
```
Create a Docker secret for the certificate:
```bash
docker secret create kestrel_certificate ~/.aspnet/https/cert.pfx
```
Both secrets are copied into the Docker container in the `docker-compose.yml` file.  The
certificate is referenced in the `ASPNETCORE_Kestrel__Certificates__Default__Path` environment
variable.  The password is loaded when the app runs by copying the
`/run/secrets/kestrel_certificates_default_password` file's contents into the Configuration
`Kestrel:Certificates:Default:Password` where it is picked up by Kestrel.
