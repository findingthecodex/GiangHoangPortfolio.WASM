#!/bin/bash

# Deploy script for Blazor WASM to GitHub Pages

echo "🔨 Building project..."
cd GiangHoangPortfolio.WASM
dotnet publish -c Release -o ./publish

if [ $? -ne 0 ]; then
    echo "❌ Build failed!"
    exit 1
fi

echo "🧹 Cleaning old publish output and root files..."
cd ..
rm -rf GiangHoangPortfolio.WASM/publish
rm -rf _framework _content css sample-data
find . -maxdepth 1 \( -name "*.css" -o -name "*.html" -o -name "*.png" -o -name "*.js" \) -delete

echo "🔨 Re-publishing after clean..."
cd GiangHoangPortfolio.WASM
dotnet publish -c Release -o ./publish
cd ..

echo "📦 Copying new files from publish folder..."
cp -r GiangHoangPortfolio.WASM/publish/wwwroot/* .

echo "📄 Creating 404.html from index.html..."
cp index.html 404.html

echo "📤 Committing and pushing to GitHub..."
git add .
git commit -m "Deploy update"
git push

echo "✅ Deploy complete!"