#!/bin/bash

# Deploy script for Blazor WASM to GitHub Pages

echo "🔨 Building project..."
cd GiangHoangPortfolio.WASM
dotnet publish -c Release -o ./publish

if [ $? -ne 0 ]; then
    echo "❌ Build failed!"
    exit 1
fi

echo "🧹 Cleaning old files in root..."
cd ..
rm -rf _framework css _content *.css *.html *.png images sample-data

echo "📦 Copying new files from publish folder..."
cp -r GiangHoangPortfolio.WASM/publish/wwwroot/* .

echo "📄 Creating 404.html from index.html..."
cp index.html 404.html

echo "📤 Committing and pushing to GitHub..."
git add .
git commit -m "Deploy update"
git push

echo "✅ Deploy complete!"