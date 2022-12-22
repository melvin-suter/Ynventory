![Ynventory](https://github.com/melvin-suter/Ynventory/raw/master/images/logo.png)

![pulls](https://img.shields.io/docker/pulls/suterdev/ynventory.frontend.svg )
![frontend-build](https://github.com/melvin-suter/ynventory/actions/workflows/frontend-build.yml/badge.svg)
![backend-build](https://github.com/melvin-suter/ynventory/actions/workflows/backend-build.yml/badge.svg)

Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.

![Example UI](images/screenshot.png)


# Deplyoment

## Kubernetes 

Use the files inside `deployments/kubernetes/` to deploy on Kubernetes

## Docker

Needed Packages:
- git
- docker
- docker-compose

Run this on your docker host, inside the folder you want to use to store data:

```bash
# Downlaod Docker Compose
git clone https://github.com/melvin-suter/Ynventory.git
mv Ynventory/deployments/docker/* ./
rm -rf Ynventory/

# Change DB Password (only run once)
sed -i "s;YNVENTORYPASSWORD;$(date | md5sum | awk '{print $1}');" docker-compose.yml

# Run Compose
docker-compose up
```

### Certificates

TBD

## Cloud Provider

TBD

# Roadmap

These are the planed versions to be released.

## Version 1.0.0

- [ ] Creating Collections, Folders, Cards, Decks
- [ ] Import Delver Files (CSV, .delver, etc. **TBD**)
- [ ] Deplyoment with Kubernetes
- [ ] Deployment with Docker Compose

## Version 1.1.0

- [ ] Mobile Friendly UI
- [ ] Example Deployments on Hostsers (Hetzner, AWS, etc. **TBD**)

# Contributers

<div>
    <a href="https://github.com/melvin-suter"><img src="https://avatars.githubusercontent.com/u/44713851?v=4" alt="https://github.com/melvin-suter" style="width:60px; border-radius: 50%"/></a>
    <a href="https://github.com/pluethi1"><img src="https://avatars.githubusercontent.com/u/32535195?v=4" alt="https://github.com/pluethi1" style="width:60px; border-radius: 50%"/></a>
</div>

# Contribute

Yes please :)

Just send us a PR.